﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToolingManWPF.StorageManageServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MoldUseType", Namespace="http://schemas.datacontract.org/2004/07/ClassLibrary.ENUM")]
    public enum MoldUseType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Produce = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Mantain = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Test = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/ToolingWCF.DataModel")]
    [System.SerializableAttribute()]
    public partial class Message : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ToolingManWPF.StorageManageServiceReference.MsgType MsgTypeField;
        
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
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ToolingManWPF.StorageManageServiceReference.MsgType MsgType {
            get {
                return this.MsgTypeField;
            }
            set {
                if ((this.MsgTypeField.Equals(value) != true)) {
                    this.MsgTypeField = value;
                    this.RaisePropertyChanged("MsgType");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MsgType", Namespace="http://schemas.datacontract.org/2004/07/ToolingWCF.DataModel")]
    public enum MsgType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OK = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Warn = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MoldReturnStateType", Namespace="http://schemas.datacontract.org/2004/07/ClassLibrary.ENUM")]
    public enum MoldReturnStateType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Normal = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NeedMantain = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Broken = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FileUP", Namespace="http://schemas.datacontract.org/2004/07/ToolingWCF.DataModel")]
    [System.SerializableAttribute()]
    public partial class FileUP : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ToolingManWPF.StorageManageServiceReference.AttachmentType TypeField;
        
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
        public byte[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileType {
            get {
                return this.FileTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.FileTypeField, value) != true)) {
                    this.FileTypeField = value;
                    this.RaisePropertyChanged("FileType");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ToolingManWPF.StorageManageServiceReference.AttachmentType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AttachmentType", Namespace="http://schemas.datacontract.org/2004/07/ClassLibrary.ENUM")]
    public enum AttachmentType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PICTURE = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DOCUMENT = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StorageManageServiceReference.IStorageManageService")]
    public interface IStorageManageService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/ApplyMold", ReplyAction="http://tempuri.org/IStorageManageService/ApplyMoldResponse")]
        ToolingManWPF.StorageManageServiceReference.Message ApplyMold(ToolingManWPF.StorageManageServiceReference.MoldUseType moldUseType, string moldNR, string applicantNR, string operatorNR, string workstationNR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/ReturnMold", ReplyAction="http://tempuri.org/IStorageManageService/ReturnMoldResponse")]
        ToolingManWPF.StorageManageServiceReference.Message ReturnMold(string moldNR, string applicantNR, string operatorNR, string remark, ToolingManWPF.StorageManageServiceReference.MoldReturnStateType moldState);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/ReturnMoldInPosition", ReplyAction="http://tempuri.org/IStorageManageService/ReturnMoldInPositionResponse")]
        ToolingManWPF.StorageManageServiceReference.Message ReturnMoldInPosition(string moldNR, string operatorNR, string remark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/MoldInStore", ReplyAction="http://tempuri.org/IStorageManageService/MoldInStoreResponse")]
        ToolingManWPF.StorageManageServiceReference.Message MoldInStore(string moldNR, string operatorNR, string warehouesNR, string positionNR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/MoldMoveStore", ReplyAction="http://tempuri.org/IStorageManageService/MoldMoveStoreResponse")]
        ToolingManWPF.StorageManageServiceReference.Message MoldMoveStore(string moldNR, string warehouseNR, string sourcePosiNr, string desiPosiNr);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/MoldTest", ReplyAction="http://tempuri.org/IStorageManageService/MoldTestResponse")]
        ToolingManWPF.StorageManageServiceReference.Message MoldTest(string moldNR, string operatorNR, System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files, int currentCutTimes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/MoldMaintain", ReplyAction="http://tempuri.org/IStorageManageService/MoldMaintainResponse")]
        ToolingManWPF.StorageManageServiceReference.Message MoldMaintain(string moldNR, string operatorNR, System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/FileUpLoad", ReplyAction="http://tempuri.org/IStorageManageService/FileUpLoadResponse")]
        ToolingManWPF.StorageManageServiceReference.Message FileUpLoad(System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files, string masterNR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/GetPartPoolPosiNr", ReplyAction="http://tempuri.org/IStorageManageService/GetPartPoolPosiNrResponse")]
        string GetPartPoolPosiNr();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStorageManageService/MoldMoveWorkStation", ReplyAction="http://tempuri.org/IStorageManageService/MoldMoveWorkStationResponse")]
        ToolingManWPF.StorageManageServiceReference.Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStorageManageServiceChannel : ToolingManWPF.StorageManageServiceReference.IStorageManageService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StorageManageServiceClient : System.ServiceModel.ClientBase<ToolingManWPF.StorageManageServiceReference.IStorageManageService>, ToolingManWPF.StorageManageServiceReference.IStorageManageService {
        
        public StorageManageServiceClient() {
        }
        
        public StorageManageServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StorageManageServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StorageManageServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StorageManageServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message ApplyMold(ToolingManWPF.StorageManageServiceReference.MoldUseType moldUseType, string moldNR, string applicantNR, string operatorNR, string workstationNR) {
            return base.Channel.ApplyMold(moldUseType, moldNR, applicantNR, operatorNR, workstationNR);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message ReturnMold(string moldNR, string applicantNR, string operatorNR, string remark, ToolingManWPF.StorageManageServiceReference.MoldReturnStateType moldState) {
            return base.Channel.ReturnMold(moldNR, applicantNR, operatorNR, remark, moldState);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message ReturnMoldInPosition(string moldNR, string operatorNR, string remark) {
            return base.Channel.ReturnMoldInPosition(moldNR, operatorNR, remark);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message MoldInStore(string moldNR, string operatorNR, string warehouesNR, string positionNR) {
            return base.Channel.MoldInStore(moldNR, operatorNR, warehouesNR, positionNR);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message MoldMoveStore(string moldNR, string warehouseNR, string sourcePosiNr, string desiPosiNr) {
            return base.Channel.MoldMoveStore(moldNR, warehouseNR, sourcePosiNr, desiPosiNr);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message MoldTest(string moldNR, string operatorNR, System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files, int currentCutTimes) {
            return base.Channel.MoldTest(moldNR, operatorNR, files, currentCutTimes);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message MoldMaintain(string moldNR, string operatorNR, System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files) {
            return base.Channel.MoldMaintain(moldNR, operatorNR, files);
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message FileUpLoad(System.Collections.Generic.List<ToolingManWPF.StorageManageServiceReference.FileUP> files, string masterNR) {
            return base.Channel.FileUpLoad(files, masterNR);
        }
        
        public string GetPartPoolPosiNr() {
            return base.Channel.GetPartPoolPosiNr();
        }
        
        public ToolingManWPF.StorageManageServiceReference.Message MoldMoveWorkStation(string moldNR, string operatorNR, string targetWStationNR) {
            return base.Channel.MoldMoveWorkStation(moldNR, operatorNR, targetWStationNR);
        }
    }
}
