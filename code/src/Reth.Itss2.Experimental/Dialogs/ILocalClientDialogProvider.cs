﻿using Reth.Itss2.Experimental.Dialogs.ArticleData.ArticlePrice;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ArticleSelected;
using Reth.Itss2.Experimental.Dialogs.SalesTransactions.ShoppingCart;

namespace Reth.Itss2.Experimental.Dialogs
{
    public interface ILocalClientDialogProvider:Reth.Itss2.Standard.Dialogs.ILocalClientDialogProvider
    {
        IArticlePriceClientDialog ArticlePrice{ get; }
        IArticleSelectedClientDialog ArticleSelected{ get; }
        IShoppingCartClientDialog ShoppingCart{ get; }
    }
}