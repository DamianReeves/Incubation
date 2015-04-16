using System;

namespace ZeroMQDemos.DomainModel
{
    public abstract class AggregateBase
    {
        public void Apply(object command)
        {
            dynamic dCommand = command;
            ApplyCore(command);
        }

        /// <exception cref="InvalidOperationException">The supplied <see cref="command"/> parameter is not a <see cref="Type"/> handled by this aggregate. </exception>
        protected virtual void ApplyCore(object command)
        {
            if (command != null)
            {
                var commandType = command.GetType();
                var msg = string.Format(@"The aggregate does not contain a handler for commands of type ""{0}"".",
                    commandType);
                throw new InvalidOperationException(msg);
            }
        }
    }
}
