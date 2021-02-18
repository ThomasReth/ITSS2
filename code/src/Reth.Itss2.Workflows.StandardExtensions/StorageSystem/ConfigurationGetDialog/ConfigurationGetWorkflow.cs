﻿// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Roles.StorageSystem;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGetDialog;
using Reth.Itss2.Workflows.Standard;

namespace Reth.Itss2.Workflows.StandardExtensions.StorageSystem.ConfigurationGetDialog
{
    internal class ConfigurationGetWorkflow:Workflow<IStorageSystemConfigurationGetDialog>, IConfigurationGetWorkflow
    {
        public ConfigurationGetWorkflow(    IStorageSystemWorkflowProvider workflowProvider,
                                            IStorageSystemDialogProvider dialogProvider,
                                            ISerializationProvider serializationProvider    )
        :
            base( workflowProvider, dialogProvider, serializationProvider, dialogProvider.ConfigurationGetDialog )
        {
            this.Dialog.RequestReceived += this.Dialog_RequestReceived;
        }

        private void Dialog_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            this.OnRequestReceived( ( ConfigurationGetRequest )e.Message,
                                    this.RequestReceived,
                                    ( ConfigurationGetResponse response ) =>
                                    {
                                        this.Dialog.SendResponse( response );
                                    }   );
        }

        public Func<ConfigurationGetRequest, ConfigurationGetResponse>? RequestReceived
        {
            get; set;
        }

        protected override void Dispose( bool disposing )
        {
        }
    }
}