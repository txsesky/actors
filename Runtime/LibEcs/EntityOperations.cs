using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Pixeye.Actors
{
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	struct EntityOperation
	{
		public ent entity;
		public int arg;
		public EntityOperations.Action action;

		public EntityOperation(in ent entity, int arg, EntityOperations.Action action)
		{
			this.entity = entity;
			this.arg    = arg;
			this.action = action;
		}
	}

	[Il2CppSetOption(Option.NullChecks | Option.ArrayBoundsChecks | Option.DivideByZeroChecks, false)]
	public static class EntityOperations
	{
		public enum Action : byte
		{
			Add = 0,
			ChangeTag,
			Remove,
			Kill,
			Empty,
			Activate
		}

		internal static EntityOperation[] operations = new EntityOperation[Framework.Settings.SizeEntities];
		public static int len;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Set(in ent entity, int arg, Action action)
		{
			if (len == operations.Length)
				Array.Resize(ref operations, len << 1);
 	
			ref var operation = ref operations[len++];
			operation.entity = entity;
			operation.arg    = arg;
			operation.action = action;
 
		}
	}
}