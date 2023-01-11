using System;
using Proj_PSSC_Main.Models;

namespace Proj_PSSC_Main.Services
{
	public interface IMainService { }

	public interface IServiceReturnType:IMainService { }

	public interface IServiceGetData:IServiceReturnType
	{
		public IServiceReturnType[] GetData();

		public void SetData(IServiceReturnType[] input);
	}
}

