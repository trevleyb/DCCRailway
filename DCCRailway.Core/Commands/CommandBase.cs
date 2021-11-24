using System;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Utilities;
using DCCRailway.Core.Validators;

namespace DCCRailway.Core.Commands {
	public abstract class CommandBase : PropertyChangedBase, ICommand {
		public abstract IResult Execute(IAdapter adapter);

		protected IResult SendAndReceieve(IAdapter adapter, IResultValidation validator, byte sendData) {
			return SendAndReceieve(adapter, validator, new[] { sendData });
		}

		protected IResult SendAndReceieve(IAdapter adapter, IResultValidation validator, byte[] sendData) {
			// Send the command provided to the command station
			// -----------------------------------------------------------------------------------------------------------
			if (adapter == null) throw new ArgumentNullException(nameof(adapter), "The adapter cannot be null.");
			adapter.SendData(sendData, this);

			// All commands with NCE have a response so read back some data
			// ------------------------------------------------------------
			var recvData = adapter.RecvData(this);
			if (recvData == null) {
				throw new ExpectedDataException(null, adapter, "Command expected to recieve data from the Adapter but recieved nothing. ");
			}

			// Validate the data. All NCE Commands return a ! for OK or another code for an error, or some data if appropriate
			// ---------------------------------------------------------------------------------------------------------------
			return validator.Validate(recvData);
		}
	}
}