﻿using System;
using System.Collections.Generic;
using System.IO;
using UtinyRipper.Classes.Shaders.Exporters;

namespace UtinyRipper.Classes.Shaders
{
	public struct SerializedPass : IAssetReadable
	{
		public void Read(AssetReader reader)
		{
			m_nameIndices = new Dictionary<string, int>();
			
			m_nameIndices.Read(reader);
			Type = (SerializedPassType)reader.ReadInt32();
			State.Read(reader);
			ProgramMask = reader.ReadUInt32();
			ProgVertex.Read(reader);
			ProgFragment.Read(reader);
			ProgGeometry.Read(reader);
			ProgHull.Read(reader);
			ProgDomain.Read(reader);
			HasInstancingVariant = reader.ReadBoolean();
			reader.AlignStream(AlignType.Align4);

			UseName = reader.ReadStringAligned();
			Name = reader.ReadStringAligned();
			TextureName = reader.ReadStringAligned();
			Tags.Read(reader);
		}

		public void Export(TextWriter writer, Shader shader, Func<ShaderGpuProgramType, ShaderTextExporter> exporterInstantiator)
		{
			writer.WriteIntent(2);
			writer.Write("{0} ", Type.ToString());

			if (Type == SerializedPassType.UsePass)
			{
				writer.Write("\"{0}\"\n", UseName);
			}
			else
			{
				writer.Write("{\n");
				
				if (Type == SerializedPassType.GrabPass)
				{
					if(TextureName != string.Empty)
					{
						writer.WriteIntent(3);
						writer.Write("\"{0}\"\n", TextureName);
					}
				}
				else if (Type == SerializedPassType.Pass)
				{
					State.Export(writer);

					ProgVertex.Export(writer, shader, ShaderType.Vertex, exporterInstantiator);
					ProgFragment.Export(writer, shader, ShaderType.Fragment, exporterInstantiator);
					ProgGeometry.Export(writer, shader, ShaderType.Geometry, exporterInstantiator);
					ProgHull.Export(writer, shader, ShaderType.Hull, exporterInstantiator);
					ProgDomain.Export(writer, shader, ShaderType.Domain, exporterInstantiator);

#warning ProgramMask?
#warning HasInstancingVariant?
				}
				else
				{
					throw new NotSupportedException($"Unsupported pass type {Type}");
				}

				writer.WriteIntent(2);
				writer.Write("}\n");
			}
		}
		
		public IReadOnlyDictionary<string, int> NameIndices => m_nameIndices;
		public SerializedPassType Type { get; private set; }
		public uint ProgramMask { get; private set; }
		public bool HasInstancingVariant { get; private set; }
		public string UseName { get; private set; }
		public string Name { get; private set; }
		public string TextureName { get; private set; }

		public SerializedShaderState State;
		public SerializedProgram ProgVertex;
		public SerializedProgram ProgFragment;
		public SerializedProgram ProgGeometry;
		public SerializedProgram ProgHull;
		public SerializedProgram ProgDomain;
		public SerializedTagMap Tags;

		private Dictionary<string, int> m_nameIndices;
	}
}
