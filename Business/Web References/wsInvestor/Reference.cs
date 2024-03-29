﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Business.wsInvestor {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSEstructurasSoap", Namespace="http://IDCE.com/")]
    public partial class WSEstructuras : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetI01OperationCompleted;
        
        private System.Threading.SendOrPostCallback GetI02OperationCompleted;
        
        private System.Threading.SendOrPostCallback GetRMLOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSEstructuras() {
            this.Url = global::Business.Properties.Settings.Default.Business_wsInvestor_WSEstructuras;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetI01CompletedEventHandler GetI01Completed;
        
        /// <remarks/>
        public event GetI02CompletedEventHandler GetI02Completed;
        
        /// <remarks/>
        public event GetRMLCompletedEventHandler GetRMLCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://IDCE.com/GetI01", RequestNamespace="http://IDCE.com/", ResponseNamespace="http://IDCE.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetI01(string fecha) {
            object[] results = this.Invoke("GetI01", new object[] {
                        fecha});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetI01Async(string fecha) {
            this.GetI01Async(fecha, null);
        }
        
        /// <remarks/>
        public void GetI01Async(string fecha, object userState) {
            if ((this.GetI01OperationCompleted == null)) {
                this.GetI01OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetI01OperationCompleted);
            }
            this.InvokeAsync("GetI01", new object[] {
                        fecha}, this.GetI01OperationCompleted, userState);
        }
        
        private void OnGetI01OperationCompleted(object arg) {
            if ((this.GetI01Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetI01Completed(this, new GetI01CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://IDCE.com/GetI02", RequestNamespace="http://IDCE.com/", ResponseNamespace="http://IDCE.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetI02(string fecha) {
            object[] results = this.Invoke("GetI02", new object[] {
                        fecha});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetI02Async(string fecha) {
            this.GetI02Async(fecha, null);
        }
        
        /// <remarks/>
        public void GetI02Async(string fecha, object userState) {
            if ((this.GetI02OperationCompleted == null)) {
                this.GetI02OperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetI02OperationCompleted);
            }
            this.InvokeAsync("GetI02", new object[] {
                        fecha}, this.GetI02OperationCompleted, userState);
        }
        
        private void OnGetI02OperationCompleted(object arg) {
            if ((this.GetI02Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetI02Completed(this, new GetI02CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://IDCE.com/GetRML", RequestNamespace="http://IDCE.com/", ResponseNamespace="http://IDCE.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable GetRML(string fechaInicio, string fechaFin) {
            object[] results = this.Invoke("GetRML", new object[] {
                        fechaInicio,
                        fechaFin});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void GetRMLAsync(string fechaInicio, string fechaFin) {
            this.GetRMLAsync(fechaInicio, fechaFin, null);
        }
        
        /// <remarks/>
        public void GetRMLAsync(string fechaInicio, string fechaFin, object userState) {
            if ((this.GetRMLOperationCompleted == null)) {
                this.GetRMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetRMLOperationCompleted);
            }
            this.InvokeAsync("GetRML", new object[] {
                        fechaInicio,
                        fechaFin}, this.GetRMLOperationCompleted, userState);
        }
        
        private void OnGetRMLOperationCompleted(object arg) {
            if ((this.GetRMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetRMLCompleted(this, new GetRMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    public delegate void GetI01CompletedEventHandler(object sender, GetI01CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetI01CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetI01CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    public delegate void GetI02CompletedEventHandler(object sender, GetI02CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetI02CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetI02CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    public delegate void GetRMLCompletedEventHandler(object sender, GetRMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3062.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetRMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetRMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591