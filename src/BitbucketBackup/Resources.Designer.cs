﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.239
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BitbucketBackup {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BitbucketBackup.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Authentication failed.
        ///Please check if the password is valid! ähnelt.
        /// </summary>
        internal static string AuthenticationFailed {
            get {
                return ResourceManager.GetString("AuthenticationFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Backup completed! ähnelt.
        /// </summary>
        internal static string BackupCompleted {
            get {
                return ResourceManager.GetString("BackupCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Backup failed! ähnelt.
        /// </summary>
        internal static string ClientExceptionHeadline {
            get {
                return ResourceManager.GetString("ClientExceptionHeadline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Local backup folder: {0} ähnelt.
        /// </summary>
        internal static string IntroFolder {
            get {
                return ResourceManager.GetString("IntroFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Bitbucket Backup {0} ähnelt.
        /// </summary>
        internal static string IntroHeadline {
            get {
                return ResourceManager.GetString("IntroHeadline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Bitbucket user: {0} ähnelt.
        /// </summary>
        internal static string IntroUser {
            get {
                return ResourceManager.GetString("IntroUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Couldn&apos;t load information for user: {0}
        ///Please check if the user name is valid! ähnelt.
        /// </summary>
        internal static string InvalidUsername {
            get {
                return ResourceManager.GetString("InvalidUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Bitbucket API didn&apos;t return a response: {0} ähnelt.
        /// </summary>
        internal static string NoResponse {
            get {
                return ResourceManager.GetString("NoResponse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Press &lt;ENTER&gt; to quit! ähnelt.
        /// </summary>
        internal static string PressEnter {
            get {
                return ResourceManager.GetString("PressEnter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die git pull: {0} ähnelt.
        /// </summary>
        internal static string PullingGit {
            get {
                return ResourceManager.GetString("PullingGit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die  hg pull: {0} ähnelt.
        /// </summary>
        internal static string PullingMercurial {
            get {
                return ResourceManager.GetString("PullingMercurial", resourceCulture);
            }
        }
    }
}
