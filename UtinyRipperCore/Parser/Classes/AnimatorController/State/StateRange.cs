﻿namespace UtinyRipper.Classes.AnimatorControllers
{
	public struct StateRange : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			StartIndex = (int)reader.ReadUInt32();
			Count = (int)reader.ReadUInt32();
		}

		public int StartIndex { get; private set; }
		public int Count { get; private set; }
	}
}
