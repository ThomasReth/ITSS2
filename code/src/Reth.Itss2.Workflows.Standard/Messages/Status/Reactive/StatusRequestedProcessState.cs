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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Status;

namespace Reth.Itss2.Workflows.Standard.Messages.Status.Reactive
{
    internal class StatusRequestedProcessState:ProcessState, IStatusRequestedProcessState
    {
        public StatusRequestedProcessState( StatusWorkflow workflow, StatusRequest request )
        {
            this.Workflow = workflow;
            this.Request = request;
        }

        private StatusWorkflow Workflow
        {
            get;
        }

        public StatusRequest Request
        {
            get;
        }

        public void Finish( ComponentState state )
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new StatusResponse( this.Request, state ) );
        }

        public void Finish( ComponentState state, String? stateText, IEnumerable<Component>? components )
        {
            this.OnStateChange();

            this.Workflow.SendResponse( new StatusResponse( this.Request, state, stateText, components ) );
        }

        public Task FinishAsync( ComponentState state, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new StatusResponse( this.Request, state ), cancellationToken );
        }

        public Task FinishAsync( ComponentState state, String? stateText, IEnumerable<Component>? components, CancellationToken cancellationToken = default )
        {
            this.OnStateChange();

            return this.Workflow.SendResponseAsync( new StatusResponse( this.Request, state, stateText, components ), cancellationToken );
        }
    }
}