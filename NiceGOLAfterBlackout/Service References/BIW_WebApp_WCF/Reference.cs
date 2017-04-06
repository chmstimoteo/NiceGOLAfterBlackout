﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NiceGOLAfterBlackout.BIW_WebApp_WCF {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BIW_WebApp_WCF.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetBodySpecCodeDetailsByEqId", ReplyAction="http://tempuri.org/IService/GetBodySpecCodeDetailsByEqIdResponse")]
        System.Data.DataSet GetBodySpecCodeDetailsByEqId(string equipmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetBodySpecCodeDetailsByEqId", ReplyAction="http://tempuri.org/IService/GetBodySpecCodeDetailsByEqIdResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetBodySpecCodeDetailsByEqIdAsync(string equipmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetNextBIWOrder", ReplyAction="http://tempuri.org/IService/GetNextBIWOrderResponse")]
        System.Data.DataSet GetNextBIWOrder(string equipmentId, int lastSequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetNextBIWOrder", ReplyAction="http://tempuri.org/IService/GetNextBIWOrderResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetNextBIWOrderAsync(string equipmentId, int lastSequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetBIWOrders", ReplyAction="http://tempuri.org/IService/GetBIWOrdersResponse")]
        System.Data.DataSet GetBIWOrders(string equipmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetBIWOrders", ReplyAction="http://tempuri.org/IService/GetBIWOrdersResponse")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetBIWOrdersAsync(string equipmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReceiveBIWEntryAckByEquipmentIdAndOrderId", ReplyAction="http://tempuri.org/IService/ReceiveBIWEntryAckByEquipmentIdAndOrderIdResponse")]
        bool ReceiveBIWEntryAckByEquipmentIdAndOrderId(string equipmentId, string orderId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReceiveBIWEntryAckByEquipmentIdAndOrderId", ReplyAction="http://tempuri.org/IService/ReceiveBIWEntryAckByEquipmentIdAndOrderIdResponse")]
        System.Threading.Tasks.Task<bool> ReceiveBIWEntryAckByEquipmentIdAndOrderIdAsync(string equipmentId, string orderId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReceiveBIWEntriesAck", ReplyAction="http://tempuri.org/IService/ReceiveBIWEntriesAckResponse")]
        bool ReceiveBIWEntriesAck(string equipmentId, int sequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/ReceiveBIWEntriesAck", ReplyAction="http://tempuri.org/IService/ReceiveBIWEntriesAckResponse")]
        System.Threading.Tasks.Task<bool> ReceiveBIWEntriesAckAsync(string equipmentId, int sequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/UpdateLastInlineSequenceNumber", ReplyAction="http://tempuri.org/IService/UpdateLastInlineSequenceNumberResponse")]
        bool UpdateLastInlineSequenceNumber(string equipmentId, int sequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/UpdateLastInlineSequenceNumber", ReplyAction="http://tempuri.org/IService/UpdateLastInlineSequenceNumberResponse")]
        System.Threading.Tasks.Task<bool> UpdateLastInlineSequenceNumberAsync(string equipmentId, int sequenceNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/KeepAlive", ReplyAction="http://tempuri.org/IService/KeepAliveResponse")]
        bool KeepAlive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/KeepAlive", ReplyAction="http://tempuri.org/IService/KeepAliveResponse")]
        System.Threading.Tasks.Task<bool> KeepAliveAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : NiceGOLAfterBlackout.BIW_WebApp_WCF.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<NiceGOLAfterBlackout.BIW_WebApp_WCF.IService>, NiceGOLAfterBlackout.BIW_WebApp_WCF.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet GetBodySpecCodeDetailsByEqId(string equipmentId) {
            return base.Channel.GetBodySpecCodeDetailsByEqId(equipmentId);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetBodySpecCodeDetailsByEqIdAsync(string equipmentId) {
            return base.Channel.GetBodySpecCodeDetailsByEqIdAsync(equipmentId);
        }
        
        public System.Data.DataSet GetNextBIWOrder(string equipmentId, int lastSequenceNumber) {
            return base.Channel.GetNextBIWOrder(equipmentId, lastSequenceNumber);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetNextBIWOrderAsync(string equipmentId, int lastSequenceNumber) {
            return base.Channel.GetNextBIWOrderAsync(equipmentId, lastSequenceNumber);
        }
        
        public System.Data.DataSet GetBIWOrders(string equipmentId) {
            return base.Channel.GetBIWOrders(equipmentId);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetBIWOrdersAsync(string equipmentId) {
            return base.Channel.GetBIWOrdersAsync(equipmentId);
        }
        
        public bool ReceiveBIWEntryAckByEquipmentIdAndOrderId(string equipmentId, string orderId) {
            return base.Channel.ReceiveBIWEntryAckByEquipmentIdAndOrderId(equipmentId, orderId);
        }
        
        public System.Threading.Tasks.Task<bool> ReceiveBIWEntryAckByEquipmentIdAndOrderIdAsync(string equipmentId, string orderId) {
            return base.Channel.ReceiveBIWEntryAckByEquipmentIdAndOrderIdAsync(equipmentId, orderId);
        }
        
        public bool ReceiveBIWEntriesAck(string equipmentId, int sequenceNumber) {
            return base.Channel.ReceiveBIWEntriesAck(equipmentId, sequenceNumber);
        }
        
        public System.Threading.Tasks.Task<bool> ReceiveBIWEntriesAckAsync(string equipmentId, int sequenceNumber) {
            return base.Channel.ReceiveBIWEntriesAckAsync(equipmentId, sequenceNumber);
        }
        
        public bool UpdateLastInlineSequenceNumber(string equipmentId, int sequenceNumber) {
            return base.Channel.UpdateLastInlineSequenceNumber(equipmentId, sequenceNumber);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateLastInlineSequenceNumberAsync(string equipmentId, int sequenceNumber) {
            return base.Channel.UpdateLastInlineSequenceNumberAsync(equipmentId, sequenceNumber);
        }
        
        public bool KeepAlive() {
            return base.Channel.KeepAlive();
        }
        
        public System.Threading.Tasks.Task<bool> KeepAliveAsync() {
            return base.Channel.KeepAliveAsync();
        }
    }
}
