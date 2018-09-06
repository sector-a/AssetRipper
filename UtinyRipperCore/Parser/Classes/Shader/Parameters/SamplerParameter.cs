﻿namespace UtinyRipper.Classes.Shaders
{
	public struct SamplerParameter : IAssetReadable
	{
		public SamplerParameter(uint sampler, int bindPoint)
		{
			Sampler = sampler;
			BindPoint = bindPoint;
		}

		public void Read(AssetReader reader)
		{
			Sampler = reader.ReadUInt32();
			BindPoint = reader.ReadInt32();
		}

		public uint Sampler { get; private set; }
		public int BindPoint { get; private set; }
	}
}
