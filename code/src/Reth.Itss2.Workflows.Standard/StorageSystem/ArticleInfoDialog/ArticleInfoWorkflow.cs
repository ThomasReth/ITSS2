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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfoDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.ArticleInfoDialog
{
    internal class ArticleInfoWorkflow:Workflow, IArticleInfoWorkflow
    {
        public ArticleInfoWorkflow( IStorageSystemWorkflowProvider workflowProvider )
        :
            base( workflowProvider )
        {
        }

        private IStorageSystemArticleInfoDialog Dialog
        {
            get{ return this.DialogProvider.ArticleInfoDialog; }
        }

        private ArticleInfoRequest CreateRequest( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            return this.CreateRequest(  (   MessageId messageId,
                                            SubscriberId localSubscriberId,
                                            SubscriberId remoteSubscriberId ) =>
                                        {
                                            return new ArticleInfoRequest(  messageId,
                                                                            localSubscriberId,
                                                                            remoteSubscriberId,
                                                                            articles    );
                                        }   );
        }

        public IArticleInfoProcess StartProcess( IEnumerable<ArticleInfoRequestArticle> articles )
        {
            ArticleInfoRequest request = this.CreateRequest( articles );

            ArticleInfoResponse response = this.SendRequest(    request,
                                                                () =>
                                                                {
                                                                    return this.Dialog.SendRequest( request );
                                                                }   );

            return new ArticleInfoProcess( request, response );
        }

        public async Task<IArticleInfoProcess> StartProcessAsync( IEnumerable<ArticleInfoRequestArticle> articles, CancellationToken cancellationToken = default )
        {
            ArticleInfoRequest request = this.CreateRequest( articles );

            ArticleInfoResponse response = await this.SendRequestAsync( request,
                                                                        () =>
                                                                        {
                                                                            return this.Dialog.SendRequestAsync( request, cancellationToken );
                                                                        }   ).ConfigureAwait( continueOnCapturedContext:false );

            return new ArticleInfoProcess( request, response );
        }
    }
}
