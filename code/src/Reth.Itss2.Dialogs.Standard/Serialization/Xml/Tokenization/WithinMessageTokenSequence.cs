// Implementation of the WWKS2 protocol.
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
using System.Buffers;
using System.Collections.Generic;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Tokenization
{
    internal class WithinMessageTokenSequence:TokenSequence
    {
        public WithinMessageTokenSequence( IEnumerable<TokenStateTransition> transitions, long position )
        :
            base( transitions, position )
        {
        }

        public override TokenState State => TokenState.WithinMessage;

        public override TokenSequence Traverse( ref ReadOnlySequence<byte> buffer )
        {
            TokenSequence result = this.Traverse( ref buffer, TokenPattern.EndOfMessage, TokenState.OutOfMessage );

            if( Object.ReferenceEquals( result, this ) )
            {
                result = this.Traverse( ref buffer, TokenPattern.BeginOfComment, TokenState.WithinComment );

                if( Object.ReferenceEquals( result, this ) )
                {
                    result = this.Traverse( ref buffer, TokenPattern.BeginOfData, TokenState.WithinData );
                }   
            }

            return result;
        }

        private TokenSequence Traverse( ref ReadOnlySequence<byte> buffer, TokenPattern pattern, TokenState toState )
        {
            TokenSequence result = this;

            TokenPatternMatch match = buffer.FirstMatch( pattern, this.Position );

            if( match.Success )
            {
                List<TokenStateTransition> transitions = new List<TokenStateTransition>( this.Transitions.Count + 1 );

                TokenStateTransition transition = new TokenStateTransition( this.State,
                                                                            toState,
                                                                            match   );

                transitions.AddRange( this.Transitions );
                transitions.Add( transition );

                result = TokenSequenceFactory.Create( toState, transitions, transition.Match.EndIndex ).Traverse( ref buffer );
            }

            return result;
        }
    }
}