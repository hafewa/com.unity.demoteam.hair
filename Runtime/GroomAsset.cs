﻿using System;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;

namespace Unity.DemoTeam.Hair
{
	[CreateAssetMenu]
	[PreferBinarySerialization]
	public class GroomAsset : ScriptableObject
	{
		public enum Type
		{
			Procedural,
			Alembic,
		}

		public enum MemoryLayout
		{
			StrandsSequential,
			StrandsInterleaved,
		}

		[Serializable]
		public struct SettingsBasic
		{
			[Tooltip("Type of generator")]
			public Type type;
			[Tooltip("Material applied to the generated groups")]
			public Material material;
			[Range(0.070f, 100.0f), Tooltip("Strand diameter in millimeters")]
			public float strandDiameter;

			public static readonly SettingsBasic defaults = new SettingsBasic()
			{
				type = Type.Procedural,
				strandDiameter = 10.0f,
			};
		}

		[Serializable]
		public struct SettingsAlembic
		{
			public AlembicStreamPlayer sourceAsset;

			public bool resampleCurves;
			[Range(3, HairSim.MAX_STRAND_PARTICLE_COUNT), Tooltip("Number of particles along each strand")]
			public int resampleParticleCount;

			public static readonly SettingsAlembic defaults = new SettingsAlembic()
			{
				resampleCurves = true,
				resampleParticleCount = 32,
			};
		}

		[Serializable]
		public struct SettingsProcedural
		{
			public enum Style
			{
				Curtain,
				Brush,
				Cap,
				StratifiedCurtain,
			}

			public Style style;
			[Range(64, HairSim.MAX_STRAND_COUNT), Tooltip("Number of strands")]
			public int strandCount;
			[Range(3, HairSim.MAX_STRAND_PARTICLE_COUNT), Tooltip("Number of particles along each strand")]
			public int strandParticleCount;
			[Range(0.001f, 5.0f), Tooltip("Strand length in meters")]
			public float strandLength;

			public static readonly SettingsProcedural defaults = new SettingsProcedural()
			{
				style = Style.Curtain,
				strandCount = 64,
				strandParticleCount = 32,
				strandLength = 0.5f,
			};
		}

		[Serializable]
		public struct StrandGroup
		{
			public int strandCount;
			public int strandParticleCount;

			[HideInInspector] public float strandLengthMin;
			[HideInInspector] public float strandLengthMax;
			[HideInInspector] public float strandLengthAvg;

			[HideInInspector] public MemoryLayout memoryLayout;

			[HideInInspector] public Vector3[] initialPositions;
			[HideInInspector] public Vector3[] initialRootPositions;
			[HideInInspector] public Vector3[] initialRootDirections;

			[HideInInspector] public Mesh meshAssetLines;
			[HideInInspector] public Mesh meshAssetRoots;
		}

		public Material defaultMaterial;

		public SettingsBasic settingsBasic = SettingsBasic.defaults;
		public SettingsAlembic settingsAlembic = SettingsAlembic.defaults;
		public SettingsProcedural settingsProcedural = SettingsProcedural.defaults;

		public StrandGroup[] strandGroups;
		public bool strandGroupsAutoBuild;

		public string checksum;

		public HairSim.SolverSettings settingsSolver = HairSim.SolverSettings.defaults;
		public HairSim.VolumeSettings settingsVolume = HairSim.VolumeSettings.defaults;
	}
}