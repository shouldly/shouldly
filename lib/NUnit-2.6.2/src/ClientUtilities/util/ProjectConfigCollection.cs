// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org.
// ****************************************************************

using System;
using System.Collections;

namespace NUnit.Util
{
	/// <summary>
	/// Summary description for ProjectConfigCollection.
	/// </summary>
	public class ProjectConfigCollection : CollectionBase
	{
		protected NUnitProject project;

		public ProjectConfigCollection( NUnitProject project ) 
		{ 
			this.project = project;
		}

		#region Properties
		public ProjectConfig this[int index]
		{
			get { return (ProjectConfig)InnerList[index]; }
		}

		public ProjectConfig this[string name]
		{
			get 
			{ 
				int index = IndexOf( name );
				return index >= 0 ? (ProjectConfig)InnerList[index]: null;
			}
		}
		#endregion

		#region Methods
		public void Add( ProjectConfig config )
		{
			List.Add( config );
			config.Project = this.project;
		}

		public void Add( string name )
		{
			Add( new ProjectConfig( name ) );
		}

		public void Remove( string name )
		{
			int index = IndexOf( name );
			if ( index >= 0 )
			{
				RemoveAt( index );
			}
		}

		private int IndexOf( string name )
		{
			for( int index = 0; index < InnerList.Count; index++ )
			{
				ProjectConfig config = (ProjectConfig)InnerList[index];
				if( config.Name == name )
					return index;
			}

			return -1;
		}

		public bool Contains( ProjectConfig config )
		{
			return InnerList.Contains( config );
		}

		public bool Contains( string name )
		{
			return IndexOf( name ) >= 0;
		}

        protected override void OnRemove(int index, object value)
        {
            if (project != null)
            {
                ProjectConfig config = value as ProjectConfig;
                project.IsDirty = true;
                if ( config.Name == project.ActiveConfigName )
                    project.HasChangesRequiringReload =  true;
            }
        }

		protected override void OnInsertComplete( int index, object obj )
		{
            if (project != null)
            {
                project.IsDirty = true;
                if (this.Count == 1)
                    project.HasChangesRequiringReload = true;
            }
		}
		#endregion
	}
}
