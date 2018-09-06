﻿using System;
using System.Collections.Generic;
using UtinyRipper.AssetExporters;
using UtinyRipper.Exporter.YAML;

namespace UtinyRipper.Classes.AnimatorControllers
{
	public struct SelectorStateConstant : IAssetReadable, IYAMLExportable
	{
		public void Read(AssetReader reader)
		{
			m_transitionConstantArray = reader.ReadArray<OffsetPtr<SelectorTransitionConstant>>();
			FullPathID = reader.ReadUInt32();
			IsEntry = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			throw new NotSupportedException();
		}

		public IReadOnlyList<OffsetPtr<SelectorTransitionConstant>> TransitionConstantArray => m_transitionConstantArray;
		public uint FullPathID { get; private set; }
		public bool IsEntry { get; private set; }

		private OffsetPtr<SelectorTransitionConstant>[] m_transitionConstantArray;
	}
}
