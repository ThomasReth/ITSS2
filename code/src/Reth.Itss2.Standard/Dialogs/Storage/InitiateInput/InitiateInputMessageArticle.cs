﻿using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputMessageArticle:IEquatable<InitiateInputMessageArticle>
    {
        public static bool operator==( InitiateInputMessageArticle left, InitiateInputMessageArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputMessageArticle left, InitiateInputMessageArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputMessageArticle left, InitiateInputMessageArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}
        
        private ArticleId id;

        private Nullable<int> maxSubItemQuantity;

        public InitiateInputMessageArticle( ArticleId id,
                                            String name,
                                            String dosageForm,
                                            String packingUnit,
                                            Nullable<int> maxSubItemQuantity,
                                            IEnumerable<InitiateInputMessagePack> packs )
        {
            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;

            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public String Name
        {
            get;
        }

        public String DosageForm
        {
            get;
        }

        public String PackingUnit
        {
            get;
        }

        public Nullable<int> MaxSubItemQuantity
        {
            get{ return this.maxSubItemQuantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.maxSubItemQuantity = value;
            }
        }

        private List<InitiateInputMessagePack> Packs
        {
            get;
        } = new List<InitiateInputMessagePack>();
        
        public InitiateInputMessagePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputMessageArticle );
		}
		
        public bool Equals( InitiateInputMessageArticle other )
		{
            return InitiateInputMessageArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}