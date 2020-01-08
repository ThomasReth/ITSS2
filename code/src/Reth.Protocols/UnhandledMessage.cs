﻿using System;
using System.Globalization;
using System.Text;

using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public sealed class UnhandledMessage:Message, IEquatable<UnhandledMessage>
    {
        public static bool operator==( UnhandledMessage left, UnhandledMessage right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( UnhandledMessage left, UnhandledMessage right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( UnhandledMessage left, UnhandledMessage right )
		{
            bool result = Protocols.Message.Equals( left, right );

            if( result == true )
            {
                result &= ( left.Reason == right.Reason );
                result &= Protocols.Message.Equals( left.Message, right.Message );
                result &= Exception.Equals( left.Exception, right.Exception );
            }

            return result;
		}

        public UnhandledMessage(    UnhandledReason reason,
                                    MessageDirection direction,
                                    IMessage message )
        :
            base( DialogName.Unhandled, MessageId.DefaultId )
        {
            message.ThrowIfNull();

            this.Timestamp = DateTimeOffset.UtcNow;

            this.Reason = reason;
            this.Direction = direction;
            this.Message = message;
        }

        public UnhandledMessage(    UnhandledReason reason,
                                    MessageDirection direction,
                                    IMessage message,
                                    Exception exception )
        :
            base( DialogName.Unhandled, MessageId.DefaultId )
        {
            message.ThrowIfNull();

            this.Timestamp = DateTimeOffset.UtcNow;

            this.Reason = reason;
            this.Direction = direction;
            this.Message = message;
            this.Exception = exception;
        }

        public DateTimeOffset Timestamp
        {
            get;
        }

        public UnhandledReason Reason
        {
            get;
        }

        public MessageDirection Direction
        {
            get;
        }

        public IMessage Message
        {
            get;
        }

        public Exception Exception
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as UnhandledMessage );
		}
		
		public bool Equals( UnhandledMessage other )
		{
            return UnhandledMessage.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.Direction );
            result.Append( ", " );
            result.Append( this.Reason );
            result.Append( ", " );
            result.Append( this.Message.GetType().FullName );
            result.Append( " (" );
            result.Append( this.Message.Id.ToString() );
            result.Append( "), " );
            result.Append( this.Timestamp.ToLocalTime().ToString( "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture ) );
            
            if( !( this.Exception is null ) )
            {
                result.Append( ", [" );
                result.Append( this.Exception );
                result.Append( "]" );
            }

            return result.ToString();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}