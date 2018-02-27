namespace AuctionHunter
{
	public static class AHInitializer
	{
		public static void Init()
		{
			if (Program.Container == null)
				Program.RegisterServices();
		}
	}
}
