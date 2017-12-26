﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfPortal.data.HeadingProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="HeadingEntity", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Entities")]
    [System.SerializableAttribute()]
    public partial class HeadingEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
    [System.SerializableAttribute()]
    public partial class ServiceException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HeadingProxy.IHeadingService")]
    public interface IHeadingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/GetHeadings", ReplyAction="http://tempuri.org/IHeadingService/GetHeadingsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.HeadingProxy.ServiceException), Action="http://tempuri.org/IHeadingService/GetHeadingsServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        InfPortal.data.HeadingProxy.HeadingEntity[] GetHeadings();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/GetHeadings", ReplyAction="http://tempuri.org/IHeadingService/GetHeadingsResponse")]
        System.Threading.Tasks.Task<InfPortal.data.HeadingProxy.HeadingEntity[]> GetHeadingsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/GetHeadingsByArticleId", ReplyAction="http://tempuri.org/IHeadingService/GetHeadingsByArticleIdResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/IHeadingService/GetHeadingsByArticleIdArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.HeadingProxy.ServiceException), Action="http://tempuri.org/IHeadingService/GetHeadingsByArticleIdServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        InfPortal.data.HeadingProxy.HeadingEntity[] GetHeadingsByArticleId(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/GetHeadingsByArticleId", ReplyAction="http://tempuri.org/IHeadingService/GetHeadingsByArticleIdResponse")]
        System.Threading.Tasks.Task<InfPortal.data.HeadingProxy.HeadingEntity[]> GetHeadingsByArticleIdAsync(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/AddHeading", ReplyAction="http://tempuri.org/IHeadingService/AddHeadingResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.HeadingProxy.ServiceException), Action="http://tempuri.org/IHeadingService/AddHeadingServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/IHeadingService/AddHeadingArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        bool AddHeading(InfPortal.data.HeadingProxy.HeadingEntity heading);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/AddHeading", ReplyAction="http://tempuri.org/IHeadingService/AddHeadingResponse")]
        System.Threading.Tasks.Task<bool> AddHeadingAsync(InfPortal.data.HeadingProxy.HeadingEntity heading);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/EditHeading", ReplyAction="http://tempuri.org/IHeadingService/EditHeadingResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/IHeadingService/EditHeadingArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.HeadingProxy.ServiceException), Action="http://tempuri.org/IHeadingService/EditHeadingServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        bool EditHeading(InfPortal.data.HeadingProxy.HeadingEntity heading);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/EditHeading", ReplyAction="http://tempuri.org/IHeadingService/EditHeadingResponse")]
        System.Threading.Tasks.Task<bool> EditHeadingAsync(InfPortal.data.HeadingProxy.HeadingEntity heading);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/DeleteHeading", ReplyAction="http://tempuri.org/IHeadingService/DeleteHeadingResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/IHeadingService/DeleteHeadingArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.HeadingProxy.ServiceException), Action="http://tempuri.org/IHeadingService/DeleteHeadingServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        bool DeleteHeading(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHeadingService/DeleteHeading", ReplyAction="http://tempuri.org/IHeadingService/DeleteHeadingResponse")]
        System.Threading.Tasks.Task<bool> DeleteHeadingAsync(System.Nullable<int> id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHeadingServiceChannel : InfPortal.data.HeadingProxy.IHeadingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HeadingServiceClient : System.ServiceModel.ClientBase<InfPortal.data.HeadingProxy.IHeadingService>, InfPortal.data.HeadingProxy.IHeadingService {
        
        public HeadingServiceClient() {
        }
        
        public HeadingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HeadingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HeadingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HeadingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public InfPortal.data.HeadingProxy.HeadingEntity[] GetHeadings() {
            return base.Channel.GetHeadings();
        }
        
        public System.Threading.Tasks.Task<InfPortal.data.HeadingProxy.HeadingEntity[]> GetHeadingsAsync() {
            return base.Channel.GetHeadingsAsync();
        }
        
        public InfPortal.data.HeadingProxy.HeadingEntity[] GetHeadingsByArticleId(System.Nullable<int> id) {
            return base.Channel.GetHeadingsByArticleId(id);
        }
        
        public System.Threading.Tasks.Task<InfPortal.data.HeadingProxy.HeadingEntity[]> GetHeadingsByArticleIdAsync(System.Nullable<int> id) {
            return base.Channel.GetHeadingsByArticleIdAsync(id);
        }
        
        public bool AddHeading(InfPortal.data.HeadingProxy.HeadingEntity heading) {
            return base.Channel.AddHeading(heading);
        }
        
        public System.Threading.Tasks.Task<bool> AddHeadingAsync(InfPortal.data.HeadingProxy.HeadingEntity heading) {
            return base.Channel.AddHeadingAsync(heading);
        }
        
        public bool EditHeading(InfPortal.data.HeadingProxy.HeadingEntity heading) {
            return base.Channel.EditHeading(heading);
        }
        
        public System.Threading.Tasks.Task<bool> EditHeadingAsync(InfPortal.data.HeadingProxy.HeadingEntity heading) {
            return base.Channel.EditHeadingAsync(heading);
        }
        
        public bool DeleteHeading(System.Nullable<int> id) {
            return base.Channel.DeleteHeading(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteHeadingAsync(System.Nullable<int> id) {
            return base.Channel.DeleteHeadingAsync(id);
        }
    }
}
