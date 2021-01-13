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
using System.Collections.Generic;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInputDialog
{
    public class InitiateInputResponseDetails:IEquatable<InitiateInputResponseDetails>
    {
        public static bool operator==( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
            return InitiateInputResponseDetails.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
			return !( InitiateInputResponseDetails.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputResponseDetails? left, InitiateInputResponseDetails? right )
		{
            bool result = EqualityComparer<int?>.Default.Equals( left?.InputSource, right?.InputSource );

            result &= ( result ? EqualityComparer<InitiateInputResponseStatus?>.Default.Equals( left?.Status, right?.Status ) : false );
            result &= ( result ? EqualityComparer<int?>.Default.Equals( left?.InputPoint, right?.InputPoint ) : false );

            return result;
		}
        
        public InitiateInputResponseDetails(    int inputSource,
                                                InitiateInputResponseStatus status   )
        :
            this( inputSource, status, null )
        {
        }

        public InitiateInputResponseDetails(    int inputSource,
                                                InitiateInputResponseStatus status,
                                                int? inputPoint )
        {
            inputSource.ThrowIfNegative();

            this.InputSource = inputSource;
            this.Status = status;
            this.InputPoint = inputPoint;
        }

        public int InputSource
        {
            get;
        }

        public InitiateInputResponseStatus Status
        {
            get;
        }

        public int? InputPoint
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputResponseDetails );
		}
		
		public bool Equals( InitiateInputResponseDetails? other )
		{
            return InitiateInputResponseDetails.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return HashCode.Combine( this.InputSource, this.Status );
		}

        public override String ToString()
        {
            return $"{ this.InputSource } ({ this.Status })";
        }
    }
}