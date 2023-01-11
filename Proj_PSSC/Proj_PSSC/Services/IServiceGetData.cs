using System;
using Proj_PSSC.Models;

namespace Proj_PSSC.Services
{
	public interface IServiceReturnType { }

	public interface IServiceGetData:IServiceReturnType
	{
		public IServiceReturnType[] GetData();

		public void SetData(IServiceReturnType[] input);
	}
}

