using NiceGOLAfterBlackout.BIW_WebApp_WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using NiceGOLAfterBlackout.Models;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace NiceGOLAfterBlackout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum AppStates { Idle, LookingInfo, ProcessingCommand };
        private enum WCFServiceResponseStatus
        {
            BIW_WebApp_WCF_OK, BIW_WebApp_WCF_NOK, BIW_SFM_AUC_OK, BIW_SFM_AUC_NOK,
            BIW_SFM_AUE_OK, BIW_SFM_AUE_NOK, BIW_SFM_TUP_OK, BIW_SFM_TUP_NOK, BIW_SFM_TPF_OK, BIW_SFM_TPF_NOK,
            BIW_SFM_TEA_OK, BIW_SFM_TEA_NOK, BIW_SFM_SPJ_OK, BIW_SFM_SPJ_NOK, BIW_SFM_SPG_OK, BIW_SFM_SPG_NOK,
            BIW_SFM_SCE_OK, BIW_SFM_SCE_NOK, BIW_SFM_SCC_OK, BIW_SFM_SCC_NOK, BIW_SFM_PRS_OK, BIW_SFM_PRS_NOK,
            BIW_SFM_PRD_OK, BIW_SFM_PRD_NOK, BIW_SFM_PPS_OK, BIW_SFM_PPS_NOK, BIW_SFM_PPP_OK, BIW_SFM_PPP_NOK,
            BIW_SFM_PPD_OK, BIW_SFM_PPD_NOK, BIW_SFM_PPCS_OK, BIW_SFM_PPCS_NOK, BIW_SFM_PPCD_OK, BIW_SFM_PPCD_NOK,
            BIW_SFM_PPA_OK, BIW_SFM_PPA_NOK, BIW_SFM_PLS_OK, BIW_SFM_PLS_NOK, BIW_SFM_PLD_OK, BIW_SFM_PLD_NOK,
            BIW_SFM_PLCS_OK, BIW_SFM_PLCS_NOK, BIW_SFM_PLCD_OK, BIW_SFM_PLCD_NOK, BIW_SFM_PAN_OK, BIW_SFM_PAN_NOK,
            BIW_SFM_OFS_OK, BIW_SFM_OFS_NOK, BIW_SFM_OFD_OK, BIW_SFM_OFD_NOK, BIW_SFM_FSA_OK, BIW_SFM_FSA_NOK,
            BIW_SFM_FPS_OK, BIW_SFM_FPS_NOK, BIW_SFM_FPD_OK, BIW_SFM_FPD_NOK, BIW_SFM_COF_OK, BIW_SFM_COF_NOK,
            BIW_SFM_FDA_OK, BIW_SFM_FDA_NOK
        };
        private AppStates _currentStatus { get; set; }
        private static BackgroundWorker _worker;
        private BIW_WebApp_WCF.IService biwWeb;
        private BIW_SFM_AUC.IBIW_SFM_GUI biwSFMAUC;
        private BIW_SFM_AUE.IBIW_SFM_GUI biwSFMAUE;
        private BIW_SFM_TUP.IBIW_SFM_GUI biwSFMTUP;
        private BIW_SFM_TPF.IBIW_SFM_GUI biwSFMTPF;
        private BIW_SFM_TEA.IBIW_SFM_GUI biwSFMTEA;
        private BIW_SFM_SPJ.IBIW_SFM_GUI biwSFMSPJ;
        private BIW_SFM_SPG.IBIW_SFM_GUI biwSFMSPG;
        private BIW_SFM_SCE.IBIW_SFM_GUI biwSFMSCE;
        private BIW_SFM_SCC.IBIW_SFM_GUI biwSFMSCC;
        private BIW_SFM_PRS.IBIW_SFM_GUI biwSFMPRS;
        private BIW_SFM_PRD.IBIW_SFM_GUI biwSFMPRD;
        private BIW_SFM_PPS.IBIW_SFM_GUI biwSFMPPS;
        private BIW_SFM_PPP.IBIW_SFM_GUI biwSFMPPP;
        private BIW_SFM_PPD.IBIW_SFM_GUI biwSFMPPD;
        private BIW_SFM_PPCS.IBIW_SFM_GUI biwSFMPPCS;
        private BIW_SFM_PPCD.IBIW_SFM_GUI biwSFMPPCD;
        private BIW_SFM_PPA.IBIW_SFM_GUI biwSFMPPA;
        private BIW_SFM_PLS.IBIW_SFM_GUI biwSFMPLS;
        private BIW_SFM_PLD.IBIW_SFM_GUI biwSFMPLD;
        private BIW_SFM_PLCS.IBIW_SFM_GUI biwSFMPLCS;
        private BIW_SFM_PLCD.IBIW_SFM_GUI biwSFMPLCD;
        private BIW_SFM_PAN.IBIW_SFM_GUI biwSFMPAN;
        private BIW_SFM_OFS.IBIW_SFM_GUI biwSFMOFS;
        private BIW_SFM_OFD.IBIW_SFM_GUI biwSFMOFD;
        private BIW_SFM_FSA.IBIW_SFM_GUI biwSFMFSA;
        private BIW_SFM_FPS.IBIW_SFM_GUI biwSFMFPS;
        private BIW_SFM_FPD.IBIW_SFM_GUI biwSFMFPD;
        private BIW_SFM_COF.IBIW_SFM_GUI biwSFMCOF;
        private BIW_SFM_FDA.IBIW_SFM_GUI biwSFMFDA;
        private List<BIW.IBIW_SFM_GUI> biws = new List<BIW.IBIW_SFM_GUI>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadingWCFREferences()
        {
            ClientSection clientSection =
    ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

            ChannelEndpointElementCollection endpointCollection =
                clientSection.ElementInformation.Properties[string.Empty].Value as ChannelEndpointElementCollection;


            biwWeb = new BIW_WebApp_WCF.ServiceClient();
            biwSFMAUC = new BIW_SFM_AUC.BIW_SFM_GUIClient();
            biwSFMAUE = new BIW_SFM_AUE.BIW_SFM_GUIClient();
            biwSFMTUP = new BIW_SFM_TUP.BIW_SFM_GUIClient();
            biwSFMTPF = new BIW_SFM_TPF.BIW_SFM_GUIClient();
            biwSFMTEA = new BIW_SFM_TEA.BIW_SFM_GUIClient();
            biwSFMSPJ = new BIW_SFM_SPJ.BIW_SFM_GUIClient();
            biwSFMSPG = new BIW_SFM_SPG.BIW_SFM_GUIClient();
            biwSFMSCE = new BIW_SFM_SCE.BIW_SFM_GUIClient();
            biwSFMSCC = new BIW_SFM_SCC.BIW_SFM_GUIClient();
            biwSFMPRS = new BIW_SFM_PRS.BIW_SFM_GUIClient();
            biwSFMPRD = new BIW_SFM_PRD.BIW_SFM_GUIClient();
            biwSFMPPS = new BIW_SFM_PPS.BIW_SFM_GUIClient();
            biwSFMPPP = new BIW_SFM_PPP.BIW_SFM_GUIClient();
            biwSFMPPD = new BIW_SFM_PPD.BIW_SFM_GUIClient();
            biwSFMPPCS = new BIW_SFM_PPCS.BIW_SFM_GUIClient();
            biwSFMPPCD = new BIW_SFM_PPCD.BIW_SFM_GUIClient();
            biwSFMPPA = new BIW_SFM_PPA.BIW_SFM_GUIClient();
            biwSFMPLS = new BIW_SFM_PLS.BIW_SFM_GUIClient();
            biwSFMPLD = new BIW_SFM_PLD.BIW_SFM_GUIClient();
            biwSFMPLCS = new BIW_SFM_PLCS.BIW_SFM_GUIClient();
            biwSFMPLCD = new BIW_SFM_PLCD.BIW_SFM_GUIClient();
            biwSFMPAN = new BIW_SFM_PAN.BIW_SFM_GUIClient();
            biwSFMOFS = new BIW_SFM_OFS.BIW_SFM_GUIClient();
            biwSFMOFD = new BIW_SFM_OFD.BIW_SFM_GUIClient();
            biwSFMFSA = new BIW_SFM_FSA.BIW_SFM_GUIClient();
            biwSFMFPS = new BIW_SFM_FPS.BIW_SFM_GUIClient();
            biwSFMFPD = new BIW_SFM_FPD.BIW_SFM_GUIClient();
            biwSFMCOF = new BIW_SFM_COF.BIW_SFM_GUIClient();
            biwSFMFDA = new BIW_SFM_FDA.BIW_SFM_GUIClient();
            var test = biwWeb.GetNextBIWOrder("C191.C191CLA.OFS_001",16007);
            var t1 = biwWeb.GetBIWOrders("C191.C191CLA.OFS_001");
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI1"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI2"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI6"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI3"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI4"));
            biws.Add(new BIW.BIW_SFM_GUIClient("IBIW_SFM_GUI5"));
        }

        // Set new application state, handling button sensitivity, labels, etc.
        private void SetAppState(AppStates newState)
        {
            switch (newState)
            {
                case AppStates.Idle:
                    this._currentStatus = AppStates.Idle;
                    // Hide progress widgets
                    this.cbAUC.IsEnabled = true;
                    this.cbAUE.IsEnabled = true;
                    this.cbCOF.IsEnabled = true;
                    this.cbFDA.IsEnabled = true;
                    this.cbFPD.IsEnabled = true;
                    this.cbFPS.IsEnabled = true;
                    this.cbFSA.IsEnabled = true;
                    this.cbOFD.IsEnabled = true;
                    this.cbOFS.IsEnabled = true;
                    this.cbPAN.IsEnabled = true;
                    this.cbPLCD.IsEnabled = true;
                    this.cbPLCS.IsEnabled = true;
                    this.cbPLD.IsEnabled = true;
                    this.cbPLS.IsEnabled = true;
                    this.cbPPA.IsEnabled = true;
                    this.cbPPCD.IsEnabled = true;
                    this.cbPPCS.IsEnabled = true;
                    this.cbPPD.IsEnabled = true;
                    this.cbPPP.IsEnabled = true;
                    this.cbPPS.IsEnabled = true;
                    this.cbPRD.IsEnabled = true;
                    this.cbPRS.IsEnabled = true;
                    this.cbSCC.IsEnabled = true;
                    this.cbSCE.IsEnabled = true;
                    this.cbSPG.IsEnabled = true;
                    this.cbSPJ.IsEnabled = true;
                    this.cbTEA.IsEnabled = true;
                    this.cbTPF.IsEnabled = true;
                    this.cbTUP.IsEnabled = true;
                    this.cbAUC.IsChecked = true;
                    this.cbAUE.IsChecked = true;
                    this.cbCOF.IsChecked = true;
                    this.cbFDA.IsChecked = true;
                    this.cbFPD.IsChecked = true;
                    this.cbFPS.IsChecked = true;
                    this.cbFSA.IsChecked = true;
                    this.cbOFD.IsChecked = true;
                    this.cbOFS.IsChecked = true;
                    this.cbPAN.IsChecked = true;
                    this.cbPLCD.IsChecked = true;
                    this.cbPLCS.IsChecked = true;
                    this.cbPLD.IsChecked = true;
                    this.cbPLS.IsChecked = true;
                    this.cbPPA.IsChecked = true;
                    this.cbPPCD.IsChecked = true;
                    this.cbPPCS.IsChecked = true;
                    this.cbPPD.IsChecked = true;
                    this.cbPPP.IsChecked = true;
                    this.cbPPS.IsChecked = true;
                    this.cbPRD.IsChecked = true;
                    this.cbPRS.IsChecked = true;
                    this.cbSCC.IsChecked = true;
                    this.cbSCE.IsChecked = true;
                    this.cbSPG.IsChecked = true;
                    this.cbSPJ.IsChecked = true;
                    this.cbTEA.IsChecked = true;
                    this.cbTPF.IsChecked = true;
                    this.cbTUP.IsChecked = true;
                    this.btStartSelected.Visibility = Visibility.Visible;
                    this.btStopSelected.Visibility = Visibility.Visible;
                    //pbCalculationProgress.IsEnabled = false;
                    //btnCancel.Visibility = Visibility.Collapsed;
                    break;

                case AppStates.LookingInfo:
                    this._currentStatus = AppStates.LookingInfo;
                    // Display progress widgets & Cancel button
                    this.cbAUC.IsEnabled = false;
                    this.cbAUE.IsEnabled = false;
                    this.cbCOF.IsEnabled = false;
                    this.cbFDA.IsEnabled = false;
                    this.cbFPD.IsEnabled = false;
                    this.cbFPS.IsEnabled = false;
                    this.cbFSA.IsEnabled = false;
                    this.cbOFD.IsEnabled = false;
                    this.cbOFS.IsEnabled = false;
                    this.cbPAN.IsEnabled = false;
                    this.cbPLCD.IsEnabled = false;
                    this.cbPLCS.IsEnabled = false;
                    this.cbPLD.IsEnabled = false;
                    this.cbPLS.IsEnabled = false;
                    this.cbPPA.IsEnabled = false;
                    this.cbPPCD.IsEnabled = false;
                    this.cbPPCS.IsEnabled = false;
                    this.cbPPD.IsEnabled = false;
                    this.cbPPP.IsEnabled = false;
                    this.cbPPS.IsEnabled = false;
                    this.cbPRD.IsEnabled = false;
                    this.cbPRS.IsEnabled = false;
                    this.cbSCC.IsEnabled = false;
                    this.cbSCE.IsEnabled = false;
                    this.cbSPG.IsEnabled = false;
                    this.cbSPJ.IsEnabled = false;
                    this.cbTEA.IsEnabled = false;
                    this.cbTPF.IsEnabled = false;
                    this.cbTUP.IsEnabled = false;
                    this.btStartSelected.Visibility = Visibility.Collapsed;
                    this.btStopSelected.Visibility = Visibility.Collapsed;
                    //pbCalculationProgress.IsEnabled = true;
                    ///pbCalculationProgress.Value = 0;
                    ///btnCancel.Visibility = Visibility.Visible;
                    break;

                case AppStates.ProcessingCommand:
                    this._currentStatus = AppStates.ProcessingCommand;
                    // Display progress widgets & Cancel button
                    this.cbAUC.IsEnabled = false;
                    this.cbAUE.IsEnabled = false;
                    this.cbCOF.IsEnabled = false;
                    this.cbFDA.IsEnabled = false;
                    this.cbFPD.IsEnabled = false;
                    this.cbFPS.IsEnabled = false;
                    this.cbFSA.IsEnabled = false;
                    this.cbOFD.IsEnabled = false;
                    this.cbOFS.IsEnabled = false;
                    this.cbPAN.IsEnabled = false;
                    this.cbPLCD.IsEnabled = false;
                    this.cbPLCS.IsEnabled = false;
                    this.cbPLD.IsEnabled = false;
                    this.cbPLS.IsEnabled = false;
                    this.cbPPA.IsEnabled = false;
                    this.cbPPCD.IsEnabled = false;
                    this.cbPPCS.IsEnabled = false;
                    this.cbPPD.IsEnabled = false;
                    this.cbPPP.IsEnabled = false;
                    this.cbPPS.IsEnabled = false;
                    this.cbPRD.IsEnabled = false;
                    this.cbPRS.IsEnabled = false;
                    this.cbSCC.IsEnabled = false;
                    this.cbSCE.IsEnabled = false;
                    this.cbSPG.IsEnabled = false;
                    this.cbSPJ.IsEnabled = false;
                    this.cbTEA.IsEnabled = false;
                    this.cbTPF.IsEnabled = false;
                    this.cbTUP.IsEnabled = false;
                    this.btStartSelected.Visibility = Visibility.Collapsed;
                    this.btStopSelected.Visibility = Visibility.Collapsed;
                    //pbCalculationProgress.IsEnabled = true;
                    ///pbCalculationProgress.Value = 0;
                    ///btnCancel.Visibility = Visibility.Visible;
                    break;
            }
        }

        void worker_DoWork_CheckingStats(object sender, DoWorkEventArgs e)
        {
            int max = (int)e.Argument;
            int done = 0;
            int progressPercentage = Convert.ToInt32(((double)done / max) * 100);
            BackgroundWorker bw = sender as BackgroundWorker;
            WCFServiceResponseStatus last;

            if (biwWeb.KeepAlive())
                last = WCFServiceResponseStatus.BIW_WebApp_WCF_OK;
            else
                last = WCFServiceResponseStatus.BIW_WebApp_WCF_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMAUC.Get_Status().StatoProcesso == BIW_SFM_AUC.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_AUC_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_AUC_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMAUE.Get_Status().StatoProcesso == BIW_SFM_AUE.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_AUE_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_AUE_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMCOF.Get_Status().StatoProcesso == BIW_SFM_COF.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_COF_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_COF_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMFDA.Get_Status().StatoProcesso == BIW_SFM_FDA.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_FDA_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_FDA_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMFPD.Get_Status().StatoProcesso == BIW_SFM_FPD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_FPD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_FPD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMFPS.Get_Status().StatoProcesso == BIW_SFM_FPS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_FPS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_FPS_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMFSA.Get_Status().StatoProcesso == BIW_SFM_FSA.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_FSA_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_FSA_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMOFD.Get_Status().StatoProcesso == BIW_SFM_OFD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_OFD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_OFD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMOFS.Get_Status().StatoProcesso == BIW_SFM_OFS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_OFS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_OFS_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPAN.Get_Status().StatoProcesso == BIW_SFM_PAN.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PAN_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PAN_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPLCD.Get_Status().StatoProcesso == BIW_SFM_PLCD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PLCD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PLCD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPLCS.Get_Status().StatoProcesso == BIW_SFM_PLCS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PLCS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PLCS_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPLD.Get_Status().StatoProcesso == BIW_SFM_PLD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PLD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PLD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPLS.Get_Status().StatoProcesso == BIW_SFM_PLS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PLS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PLS_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPA.Get_Status().StatoProcesso == BIW_SFM_PPA.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPA_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPA_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPCD.Get_Status().StatoProcesso == BIW_SFM_PPCD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPCD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPCD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPCS.Get_Status().StatoProcesso == BIW_SFM_PPCS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPCS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPCS_NOK;
            done++;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPD.Get_Status().StatoProcesso == BIW_SFM_PPD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPP.Get_Status().StatoProcesso == BIW_SFM_PPP.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPP_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPP_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPPS.Get_Status().StatoProcesso == BIW_SFM_PPS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PPS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PPS_OK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPRD.Get_Status().StatoProcesso == BIW_SFM_PRD.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PRD_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PRD_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMPRS.Get_Status().StatoProcesso == BIW_SFM_PRS.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_PRS_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_PRS_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMSCC.Get_Status().StatoProcesso == BIW_SFM_SCC.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_SCC_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_SCC_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMSCE.Get_Status().StatoProcesso == BIW_SFM_SCE.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_SCE_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_SCE_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMSPG.Get_Status().StatoProcesso == BIW_SFM_SPG.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_SPG_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_SPG_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMSPJ.Get_Status().StatoProcesso == BIW_SFM_SPJ.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_SPJ_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_SPJ_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMTEA.Get_Status().StatoProcesso == BIW_SFM_TEA.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_TEA_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_TEA_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMTPF.Get_Status().StatoProcesso == BIW_SFM_TPF.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_TPF_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_TPF_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            if (biwSFMTUP.Get_Status().StatoProcesso == BIW_SFM_TUP.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                last = WCFServiceResponseStatus.BIW_SFM_TUP_OK;
            else
                last = WCFServiceResponseStatus.BIW_SFM_TUP_NOK;
            progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
            bw.ReportProgress(progressPercentage, last);
            if (bw.CancellationPending)
                e.Cancel = true;

            foreach (var biw in biws)
            {

                if (biw.Get_Status().StatoProcesso == BIW.BIW_SFM_ActivitySTATO_PROCESSO.AVVIATO)
                    last = WCFServiceResponseStatus.BIW_SFM_TUP_OK;
                else
                    last = WCFServiceResponseStatus.BIW_SFM_TUP_NOK;
                progressPercentage = Convert.ToInt32(((double)done++ / max) * 100);
                bw.ReportProgress(progressPercentage, last);
                if (bw.CancellationPending)
                    e.Cancel = true;
            }


            e.Result = done;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //lock (pbCalculationProgress)
            //{
            //    pbCalculationProgress.Value = e.ProgressPercentage;
            //}
            if (e.UserState != null)
            {
                switch ((WCFServiceResponseStatus)e.UserState)
                {
                    case WCFServiceResponseStatus.BIW_WebApp_WCF_OK:
                        lock (lGOLWebServerStatus)
                        {
                            lGOLWebServerStatus.Content = "BIW Server OK";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_WebApp_WCF_NOK:
                        lock (lGOLWebServerStatus)
                        {
                            lGOLWebServerStatus.Content = "BIW Server NOK";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_AUC_OK:
                        lock (lStatusAUC)
                        {
                            lStatusAUC.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_AUC_NOK:
                        lock (lStatusAUC)
                        {
                            lStatusAUC.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_AUE_OK:
                        lock (lStatusAUE)
                        {
                            lStatusAUE.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_COF_NOK:
                        lock (lStatusCOF)
                        {
                            lStatusCOF.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_COF_OK:
                        lock (lStatusCOF)
                        {
                            lStatusCOF.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FDA_NOK:
                        lock (lStatusFDA)
                        {
                            lStatusFDA.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FDA_OK:
                        lock (lStatusFDA)
                        {
                            lStatusFDA.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FPD_NOK:
                        lock (lStatusFPD)
                        {
                            lStatusFPD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FPD_OK:
                        lock (lStatusFPD)
                        {
                            lStatusFPD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FPS_NOK:
                        lock (lStatusFPS)
                        {
                            lStatusFPS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FPS_OK:
                        lock (lStatusFPS)
                        {
                            lStatusFPS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FSA_NOK:
                        lock (lStatusFSA)
                        {
                            lStatusFSA.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_FSA_OK:
                        lock (lStatusFSA)
                        {
                            lStatusFSA.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_OFD_NOK:
                        lock (lStatusOFD)
                        {
                            lStatusOFD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_OFD_OK:
                        lock (lStatusOFD)
                        {
                            lStatusOFD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_OFS_NOK:
                        lock (lStatusOFS)
                        {
                            lStatusOFS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_OFS_OK:
                        lock (lStatusOFS)
                        {
                            lStatusOFS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PAN_NOK:
                        lock (lStatusPAN)
                        {
                            lStatusPAN.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PAN_OK:
                        lock (lStatusPAN)
                        {
                            lStatusPAN.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLCD_NOK:
                        lock (lStatusPLCD)
                        {
                            lStatusPLCD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLCD_OK:
                        lock (lStatusPLCD)
                        {
                            lStatusPLCD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLCS_NOK:
                        lock (lStatusPLCS)
                        {
                            lStatusPLCS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLCS_OK:
                        lock (lStatusPLCS)
                        {
                            lStatusPLCS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLD_NOK:
                        lock (lStatusPLD)
                        {
                            lStatusPLD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLD_OK:
                        lock (lStatusPLD)
                        {
                            lStatusPLD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLS_NOK:
                        lock (lStatusPLS)
                        {
                            lStatusPLS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PLS_OK:
                        lock (lStatusPLS)
                        {
                            lStatusPLS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPA_NOK:
                        lock (lStatusPPA)
                        {
                            lStatusPPA.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPA_OK:
                        lock (lStatusPPA)
                        {
                            lStatusPPA.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPCD_NOK:
                        lock (lStatusPPCD)
                        {
                            lStatusPPCD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPCD_OK:
                        lock (lStatusPPCD)
                        {
                            lStatusPPCD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPCS_NOK:
                        lock (lStatusPPCS)
                        {
                            lStatusPPCS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPCS_OK:
                        lock (lStatusPPCS)
                        {
                            lStatusPPCS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPD_NOK:
                        lock (lStatusPPD)
                        {
                            lStatusPPD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPD_OK:
                        lock (lStatusPPD)
                        {
                            lStatusPPD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPP_NOK:
                        lock (lStatusPPP)
                        {
                            lStatusPPP.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPP_OK:
                        lock (lStatusPPP)
                        {
                            lStatusPPP.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPS_NOK:
                        lock (lStatusPPS)
                        {
                            lStatusPPS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PPS_OK:
                        lock (lStatusPPS)
                        {
                            lStatusPPS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PRD_NOK:
                        lock (lStatusPRD)
                        {
                            lStatusPRD.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PRD_OK:
                        lock (lStatusPRD)
                        {
                            lStatusPRD.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PRS_NOK:
                        lock (lStatusPRS)
                        {
                            lStatusPRS.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_PRS_OK:
                        lock (lStatusPRS)
                        {
                            lStatusPRS.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SCC_NOK:
                        lock (lStatusSCC)
                        {
                            lStatusSCC.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SCC_OK:
                        lock (lStatusSCC)
                        {
                            lStatusSCC.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SCE_NOK:
                        lock (lStatusSCE)
                        {
                            lStatusSCE.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SCE_OK:
                        lock (lStatusSCE)
                        {
                            lStatusSCE.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SPG_NOK:
                        lock (lStatusSPG)
                        {
                            lStatusSPG.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SPG_OK:
                        lock (lStatusSPG)
                        {
                            lStatusSPG.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SPJ_NOK:
                        lock (lStatusSPJ)
                        {
                            lStatusSPJ.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_SPJ_OK:
                        lock (lStatusSPJ)
                        {
                            lStatusSPJ.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TEA_NOK:
                        lock (lStatusTEA)
                        {
                            lStatusTEA.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TEA_OK:
                        lock (lStatusTEA)
                        {
                            lStatusTEA.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TPF_NOK:
                        lock (lStatusTPF)
                        {
                            lStatusTPF.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TPF_OK:
                        lock (lStatusTPF)
                        {
                            lStatusTPF.Content = "AVVIATO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TUP_NOK:
                        lock (lStatusTUP)
                        {
                            lStatusTUP.Content = "FERMO";
                        }
                        break;
                    case WCFServiceResponseStatus.BIW_SFM_TUP_OK:
                        lock (lStatusTUP)
                        {
                            lStatusTUP.Content = "AVVIATO";
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    MessageBox.Show(e.Error.Message, "Error Executing Task");
                else if (e.Cancelled)
                    MessageBox.Show("** Cancelled **");
                else
                    MessageBox.Show("Number of Lines Cheched: " + e.Result);
            }
            finally
            {
                // State now goes back to idle
                SetAppState(AppStates.Idle);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_worker != null)
                _worker.CancelAsync();
        }

        private void btMarkUnmark_Click(object sender, RoutedEventArgs e)
        {

            Button bt = (Button)sender;
            if (bt.Content.Equals("Unmark All"))
            {
                this.cbAUC.IsChecked = false;
                this.cbAUE.IsChecked = false;
                this.cbCOF.IsChecked = false;
                this.cbFDA.IsChecked = false;
                this.cbFPD.IsChecked = false;
                this.cbFPS.IsChecked = false;
                this.cbFSA.IsChecked = false;
                this.cbOFD.IsChecked = false;
                this.cbOFS.IsChecked = false;
                this.cbPAN.IsChecked = false;
                this.cbPLCD.IsChecked = false;
                this.cbPLCS.IsChecked = false;
                this.cbPLD.IsChecked = false;
                this.cbPLS.IsChecked = false;
                this.cbPPA.IsChecked = false;
                this.cbPPCD.IsChecked = false;
                this.cbPPCS.IsChecked = false;
                this.cbPPD.IsChecked = false;
                this.cbPPP.IsChecked = false;
                this.cbPPS.IsChecked = false;
                this.cbPRD.IsChecked = false;
                this.cbPRS.IsChecked = false;
                this.cbSCC.IsChecked = false;
                this.cbSCE.IsChecked = false;
                this.cbSPG.IsChecked = false;
                this.cbSPJ.IsChecked = false;
                this.cbTEA.IsChecked = false;
                this.cbTPF.IsChecked = false;
                this.cbTUP.IsChecked = false;
                bt.Content = "Mark All";
            }
            else
            {
                this.cbAUC.IsChecked = true;
                this.cbAUE.IsChecked = true;
                this.cbCOF.IsChecked = true;
                this.cbFDA.IsChecked = true;
                this.cbFPD.IsChecked = true;
                this.cbFPS.IsChecked = true;
                this.cbFSA.IsChecked = true;
                this.cbOFD.IsChecked = true;
                this.cbOFS.IsChecked = true;
                this.cbPAN.IsChecked = true;
                this.cbPLCD.IsChecked = true;
                this.cbPLCS.IsChecked = true;
                this.cbPLD.IsChecked = true;
                this.cbPLS.IsChecked = true;
                this.cbPPA.IsChecked = true;
                this.cbPPCD.IsChecked = true;
                this.cbPPCS.IsChecked = true;
                this.cbPPD.IsChecked = true;
                this.cbPPP.IsChecked = true;
                this.cbPPS.IsChecked = true;
                this.cbPRD.IsChecked = true;
                this.cbPRS.IsChecked = true;
                this.cbSCC.IsChecked = true;
                this.cbSCE.IsChecked = true;
                this.cbSPG.IsChecked = true;
                this.cbSPJ.IsChecked = true;
                this.cbTEA.IsChecked = true;
                this.cbTPF.IsChecked = true;
                this.cbTUP.IsChecked = true;
                bt.Content = "Unmark All";
            }

        }

        private void btStartSelected_Click(object sender, RoutedEventArgs e)
        {
            SetAppState(AppStates.ProcessingCommand);
            List<String> allCbChecked = getAllCheckboxChecked();
            foreach (String item in allCbChecked)
            {
                switch (item)
                {
                    case "AUC":
                        BIW_SFM_AUC.IBIW_SFM_GUI biwSFMAUC = new BIW_SFM_AUC.BIW_SFM_GUIClient();
                        if (biwSFMAUC.StartProcess())
                            lStatusAUC.Content = "AVVIATO";
                        else
                            lStatusAUC.Content = "FERMO";
                        break;
                    default:
                        break;
                }
            }
            SetAppState(AppStates.Idle);

        }

        private List<String> getAllCheckboxChecked()
        {
            bool flag = true;
            List<String> result = new List<String>();
            if (this.cbAUC.IsChecked.Equals(flag))
                result.Add(this.cbAUC.Content.ToString());
            if (this.cbAUE.IsChecked.Equals(flag))
                result.Add(this.cbAUE.Content.ToString());
            if (this.cbCOF.IsChecked.Equals(flag))
                result.Add(this.cbCOF.Content.ToString());
            if (this.cbFDA.IsChecked.Equals(flag))
                result.Add(this.cbFDA.Content.ToString());
            if (this.cbFPD.IsChecked.Equals(flag))
                result.Add(this.cbFPD.Content.ToString());
            if (this.cbFPS.IsChecked.Equals(flag))
                result.Add(this.cbFPS.Content.ToString());
            if (this.cbFSA.IsChecked.Equals(flag))
                result.Add(this.cbFSA.Content.ToString());
            if (this.cbOFD.IsChecked.Equals(flag))
                result.Add(this.cbOFD.Content.ToString());
            if (this.cbOFS.IsChecked.Equals(flag))
                result.Add(this.cbOFS.Content.ToString());
            if (this.cbPAN.IsChecked.Equals(flag))
                result.Add(this.cbPAN.Content.ToString());
            if (this.cbPLCD.IsChecked.Equals(flag))
                result.Add(this.cbPLCD.Content.ToString());
            if (this.cbPLCS.IsChecked.Equals(flag))
                result.Add(this.cbPLCS.Content.ToString());
            if (this.cbPLD.IsChecked.Equals(flag))
                result.Add(this.cbPLD.Content.ToString());
            if (this.cbPLS.IsChecked.Equals(flag))
                result.Add(this.cbPLS.Content.ToString());
            if (this.cbPPA.IsChecked.Equals(flag))
                result.Add(this.cbPPA.Content.ToString());
            if (this.cbPPCD.IsChecked.Equals(flag))
                result.Add(this.cbPPCD.Content.ToString());
            if (this.cbPPCS.IsChecked.Equals(flag))
                result.Add(this.cbPPCS.Content.ToString());
            if (this.cbPPD.IsChecked.Equals(flag))
                result.Add(this.cbPPD.Content.ToString());
            if (this.cbPPP.IsChecked.Equals(flag))
                result.Add(this.cbPPP.Content.ToString());
            if (this.cbPPS.IsChecked.Equals(flag))
                result.Add(this.cbPPS.Content.ToString());
            if (this.cbPRD.IsChecked.Equals(flag))
                result.Add(this.cbPRD.Content.ToString());
            if (this.cbPRS.IsChecked.Equals(flag))
                result.Add(this.cbPRS.Content.ToString());
            if (this.cbSCC.IsChecked.Equals(flag))
                result.Add(this.cbSCC.Content.ToString());
            if (this.cbSCE.IsChecked.Equals(flag))
                result.Add(this.cbSCE.Content.ToString());
            if (this.cbSPG.IsChecked.Equals(flag))
                result.Add(this.cbSPG.Content.ToString());
            if (this.cbSPJ.IsChecked.Equals(flag))
                result.Add(this.cbSPJ.Content.ToString());
            if (this.cbTEA.IsChecked.Equals(flag))
                result.Add(this.cbTEA.Content.ToString());
            if (this.cbTPF.IsChecked.Equals(flag))
                result.Add(this.cbTPF.Content.ToString());
            if (this.cbTUP.IsChecked.Equals(flag))
                result.Add(this.cbTUP.Content.ToString());

            return result;
        }

        private void btStopSelected_Click(object sender, RoutedEventArgs e)
        {
            SetAppState(AppStates.ProcessingCommand);
            List<String> allCbChecked = getAllCheckboxChecked();
            foreach (String item in allCbChecked)
            {
                switch (item)
                {
                    case "AUC":
                        BIW_SFM_AUC.IBIW_SFM_GUI biwSFMAUC = new BIW_SFM_AUC.BIW_SFM_GUIClient();
                        if (biwSFMAUC.StopProcess())
                            lStatusAUC.Content = "FERMO";
                        else
                            lStatusAUC.Content = "AVVIATO";
                        break;
                    default:
                        break;
                }
            }
            SetAppState(AppStates.Idle);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            LoadingWCFREferences();
            SetAppState(AppStates.LookingInfo);
            _worker = SgMThBgWorker.getInstance;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += worker_DoWork_CheckingStats;
            _worker.ProgressChanged += worker_ProgressChanged;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerAsync(30);
        }
    }
}
