// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace NUnit.Core.Tests
{
	[TestFixture]
	public class SerializationBug
	{
		private static readonly string fileName = Path.GetTempFileName();

		[Serializable] 
		public class Perob 
		{ 
			public int i; 
			public int j; 
			public Perob(int ii,int jj) 
			{ 
				i = ii; 
				j = jj; 
			} 

			public void Serialize(string filename) 
			{ 
				StreamWriter stm = new StreamWriter(File.OpenWrite( filename )); 
				BinaryFormatter bf=new BinaryFormatter(); 
				bf.Serialize(stm.BaseStream,this); 
				stm.Close(); 
			} 
			public static Perob Deserialize(string filename) 
			{ 
				Perob rv; 
				using (StreamReader stm = new StreamReader(File.OpenRead( filename ))) 
				{
					BinaryFormatter bf=new BinaryFormatter(); 
					object obj = bf.Deserialize(stm.BaseStream);
					rv = obj as Perob; 
				}
				return rv; 
			}
		} 

		[TearDown]
		public void CleanUp()
		{
			FileInfo file = new FileInfo(fileName);
			if(file.Exists)
				file.Delete();
		}

		[Test] 
		public void SaveAndLoad() 
		{ 
			Perob p = new Perob(3,4); 
			p.Serialize( fileName ); 

			Perob np = Perob.Deserialize( fileName ); 

			Assert.IsNotNull(np, "np != null"); 
			Assert.AreEqual(p.i, np.i, "i neq" ); 
			Assert.AreEqual(p.j, np.j, "j neq" ); 
		} 
	}
}

