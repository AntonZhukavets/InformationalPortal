﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfPortal.data.LanguageProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LanguageEntity", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Entities")]
    [System.SerializableAttribute()]
    public partial class LanguageEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LanguageIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageNameField;
        
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
        public int LanguageId {
            get {
                return this.LanguageIdField;
            }
            set {
                if ((this.LanguageIdField.Equals(value) != true)) {
                    this.LanguageIdField = value;
                    this.RaisePropertyChanged("LanguageId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LanguageName {
            get {
                return this.LanguageNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageNameField, value) != true)) {
                    this.LanguageNameField = value;
                    this.RaisePropertyChanged("LanguageName");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LanguageProxy.ILanguageService")]
    public interface ILanguageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/GetLanguages", ReplyAction="http://tempuri.org/ILanguageService/GetLanguagesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.LanguageProxy.ServiceException), Action="http://tempuri.org/ILanguageService/GetLanguagesServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        InfPortal.data.LanguageProxy.LanguageEntity[] GetLanguages();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/GetLanguages", ReplyAction="http://tempuri.org/ILanguageService/GetLanguagesResponse")]
        System.Threading.Tasks.Task<InfPortal.data.LanguageProxy.LanguageEntity[]> GetLanguagesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/AddLanguage", ReplyAction="http://tempuri.org/ILanguageService/AddLanguageResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/ILanguageService/AddLanguageArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.LanguageProxy.ServiceException), Action="http://tempuri.org/ILanguageService/AddLanguageServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        bool AddLanguage(InfPortal.data.LanguageProxy.LanguageEntity language);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/AddLanguage", ReplyAction="http://tempuri.org/ILanguageService/AddLanguageResponse")]
        System.Threading.Tasks.Task<bool> AddLanguageAsync(InfPortal.data.LanguageProxy.LanguageEntity language);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/RestoreLanguage", ReplyAction="http://tempuri.org/ILanguageService/RestoreLanguageResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.LanguageProxy.ServiceException), Action="http://tempuri.org/ILanguageService/RestoreLanguageServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/ILanguageService/RestoreLanguageArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        bool RestoreLanguage(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/RestoreLanguage", ReplyAction="http://tempuri.org/ILanguageService/RestoreLanguageResponse")]
        System.Threading.Tasks.Task<bool> RestoreLanguageAsync(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/DeleteLanguage", ReplyAction="http://tempuri.org/ILanguageService/DeleteLanguageResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ArgumentException), Action="http://tempuri.org/ILanguageService/DeleteLanguageArgumentExceptionFault", Name="ArgumentException", Namespace="http://schemas.datacontract.org/2004/07/System")]
        [System.ServiceModel.FaultContractAttribute(typeof(InfPortal.data.LanguageProxy.ServiceException), Action="http://tempuri.org/ILanguageService/DeleteLanguageServiceExceptionFault", Name="ServiceException", Namespace="http://schemas.datacontract.org/2004/07/InfPortal.service.Business.Exceptions")]
        bool DeleteLanguage(System.Nullable<int> id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILanguageService/DeleteLanguage", ReplyAction="http://tempuri.org/ILanguageService/DeleteLanguageResponse")]
        System.Threading.Tasks.Task<bool> DeleteLanguageAsync(System.Nullable<int> id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILanguageServiceChannel : InfPortal.data.LanguageProxy.ILanguageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LanguageServiceClient : System.ServiceModel.ClientBase<InfPortal.data.LanguageProxy.ILanguageService>, InfPortal.data.LanguageProxy.ILanguageService {
        
        public LanguageServiceClient() {
        }
        
        public LanguageServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LanguageServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LanguageServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LanguageServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public InfPortal.data.LanguageProxy.LanguageEntity[] GetLanguages() {
            return base.Channel.GetLanguages();
        }
        
        public System.Threading.Tasks.Task<InfPortal.data.LanguageProxy.LanguageEntity[]> GetLanguagesAsync() {
            return base.Channel.GetLanguagesAsync();
        }
        
        public bool AddLanguage(InfPortal.data.LanguageProxy.LanguageEntity language) {
            return base.Channel.AddLanguage(language);
        }
        
        public System.Threading.Tasks.Task<bool> AddLanguageAsync(InfPortal.data.LanguageProxy.LanguageEntity language) {
            return base.Channel.AddLanguageAsync(language);
        }
        
        public bool RestoreLanguage(System.Nullable<int> id) {
            return base.Channel.RestoreLanguage(id);
        }
        
        public System.Threading.Tasks.Task<bool> RestoreLanguageAsync(System.Nullable<int> id) {
            return base.Channel.RestoreLanguageAsync(id);
        }
        
        public bool DeleteLanguage(System.Nullable<int> id) {
            return base.Channel.DeleteLanguage(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteLanguageAsync(System.Nullable<int> id) {
            return base.Channel.DeleteLanguageAsync(id);
        }
    }
}
