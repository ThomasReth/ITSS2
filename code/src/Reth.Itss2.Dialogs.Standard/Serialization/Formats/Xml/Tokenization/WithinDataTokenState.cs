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
using System.Buffers;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Serialization.Tokenization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Tokenization
{
    internal class WithinDataTokenState:XmlTokenState
    {
        public WithinDataTokenState()
        {
        }

        public override ITokenStateTransition? GetNextTransition(   ITokenStateTransition? previousTransition,
                                                                    ref ReadOnlySequence<byte> buffer,
                                                                    ref long position   )
        {
            if( previousTransition is null )
            {
                throw Assert.Exception( new ArgumentNullException( nameof( previousTransition ), $"Last transition must not be empty, if state is '{ this.ToString() }'." ) );
            }

            ITokenStateTransition? result = null;

            ITokenPatternMatch? match = XmlTokenPatterns.EndOfData.GetFirstMatch( ref buffer, position );

            if( match is not null )
            {
                position = match.EndIndex;

                result = this.GetNextTransition( previousTransition, match, ref position );
            }

            return result;
        }

        public override String ToString()
        {
            return "within string";
        }
    }
}
