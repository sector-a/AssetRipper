﻿using UtinyRipper.Classes.AnimationClips;

namespace UtinyRipper.Classes.NewAnimationTracks
{
	public struct Channel : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			ByteOffset = reader.ReadInt32();
			Curve.Read(reader);
			AttributeName = reader.ReadStringAligned();
		}

		public int ByteOffset { get; private set; }
		public string AttributeName { get; private set; }

		public AnimationCurveTpl<Float> Curve;
	}
}
