// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org.
// ****************************************************************
using System;
using System.Reflection;
using System.Text;
using System.IO;

namespace NUnit.Core
{
	/// <summary>
	/// AssemblyReader knows how to find various things in an assembly header
	/// </summary>
	public class AssemblyReader : IDisposable
	{
		private string assemblyPath;
		private BinaryReader rdr;
		private FileStream fs;

		UInt16 dos_magic = 0xffff;
		UInt32 pe_signature = 0xffffffff;
		UInt16 numberOfSections;
		UInt16 optionalHeaderSize;
        UInt16 peType;
        UInt32 numDataDirectoryEntries;

		private uint peHeader = 0;
		private uint fileHeader = 0;
		private uint optionalHeader = 0;
		private uint dataDirectory = 0;
		private uint dataSections = 0;

		private struct DataSection
		{
			public uint virtualAddress;
			public uint virtualSize;
			public uint fileOffset;
		};

		private DataSection[] sections;

		public AssemblyReader( string assemblyPath )
		{
			this.assemblyPath = assemblyPath;
			CalcHeaderOffsets();
		}

		public AssemblyReader( Assembly assembly )
		{
			this.assemblyPath = AssemblyHelper.GetAssemblyPath( assembly );
			CalcHeaderOffsets();
		}

		private void CalcHeaderOffsets()
		{
			this.fs = new FileStream( assemblyPath, FileMode.Open, FileAccess.Read );
			this.rdr = new BinaryReader( fs );
			dos_magic = rdr.ReadUInt16();
			if ( dos_magic == 0x5a4d )
			{
				fs.Position = 0x3c;
				peHeader = rdr.ReadUInt32();
				fileHeader = peHeader + 4;
				optionalHeader = fileHeader + 20;

                fs.Position = optionalHeader;
                peType = rdr.ReadUInt16();

                dataDirectory = peType == 0x20b
                    ? optionalHeader + 112
                    : optionalHeader + 96;

                fs.Position = dataDirectory - 4;
                numDataDirectoryEntries = rdr.ReadUInt32();

				fs.Position = peHeader;
				pe_signature = rdr.ReadUInt32();
				rdr.ReadUInt16(); // machine
				numberOfSections = rdr.ReadUInt16();
				fs.Position += 12;
				optionalHeaderSize = rdr.ReadUInt16();
				dataSections = optionalHeader + optionalHeaderSize;

				sections = new DataSection[numberOfSections];
				fs.Position = dataSections;
				for( int i = 0; i < numberOfSections; i++ )
				{
					fs.Position += 8;
					sections[i].virtualSize = rdr.ReadUInt32();
					sections[i].virtualAddress = rdr.ReadUInt32();
					uint rawDataSize = rdr.ReadUInt32();
					sections[i].fileOffset = rdr.ReadUInt32();
					if ( sections[i].virtualSize == 0 )
						sections[i].virtualSize = rawDataSize;

					fs.Position += 16;
				}
			}
		}

		private uint DataDirectoryRva( int n )
		{
			fs.Position = dataDirectory + n * 8;
			return rdr.ReadUInt32();
		}

		private uint RvaToLfa( uint rva )
		{
			for( int i = 0; i < numberOfSections; i++ )
				if ( rva >= sections[i].virtualAddress && rva < sections[i].virtualAddress + sections[i].virtualSize )
					return rva - sections[i].virtualAddress + sections[i].fileOffset;

			return 0;
		}

		public string AssemblyPath
		{
			get { return assemblyPath; }
		}

		public bool IsValidPeFile
		{
			get { return dos_magic == 0x5a4d && pe_signature == 0x00004550; }
		}

		public bool IsDotNetFile
		{
			get { return IsValidPeFile && numDataDirectoryEntries > 14 && DataDirectoryRva(14) != 0; }
		}

        public bool Is64BitImage
        {
            get { return peType == 0x20b; }
        }

		public string ImageRuntimeVersion
		{
			get 
			{
				string runtimeVersion = string.Empty;

                if (this.IsDotNetFile)
                {
                    uint rva = DataDirectoryRva(14);
                    if (rva != 0)
                    {
                        fs.Position = RvaToLfa(rva) + 8;
                        uint metadata = rdr.ReadUInt32();
                        fs.Position = RvaToLfa(metadata);
                        if (rdr.ReadUInt32() == 0x424a5342)
                        {
                            // Copy string representing runtime version
                            fs.Position += 12;
                            StringBuilder sb = new StringBuilder();
                            char c;
                            while ((c = rdr.ReadChar()) != '\0')
                                sb.Append(c);

                            if (sb[0] == 'v') // Last sanity check
                                runtimeVersion = sb.ToString();

                            // Could do fixups here for bad values in older files
                            // like 1.x86, 1.build, etc. But we are only using
                            // the major version anyway
                        }
                    }
                }

				return runtimeVersion; 
			}
		}

		public void Dispose()
		{
			if ( fs != null )
				fs.Close();
			if ( rdr != null )
				rdr.Close();

			fs = null;
			rdr = null;
		}
	}
}
