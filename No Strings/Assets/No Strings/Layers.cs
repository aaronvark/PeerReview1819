// This file is dynamically created and overwritten. Changes should be made in LayersWriter.cs.
using System.Linq;

namespace UnityEngine
{
	public class Layers
	{
		public const string DefaultConst = @"Default";
		public readonly string Default = @"Default";
		
		public const string TransparentFXConst = @"TransparentFX";
		public readonly string TransparentFX = @"TransparentFX";
		
		public const string IgnoreRaycastConst = @"Ignore Raycast";
		public readonly string IgnoreRaycast = @"Ignore Raycast";
		
		public const string WaterConst = @"Water";
		public readonly string Water = @"Water";
		
		public const string UIConst = @"UI";
		public readonly string UI = @"UI";
		
		public const string aConst = @"a";
		public readonly string a = @"a";
		
		/// <summary>
		/// Returns the shared layers.
		/// </summary>
		public static int Intersect(int layer1, int layer2, params int[] otherLayers) => otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current & next);
		
		/// <summary>
		/// Returns all layers as one.
		/// </summary>
		public static int Combine(int layer1, int layer2, params int[] otherLayers) => otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current | next);
		
		/// <summary>
		/// Returns the exclusive layers.
		/// </summary>
		public static int Difference(int layer1, int layer2) => layer1 ^ layer2;
		
		/// <summary>
		/// Returns all other layers.
		/// </summary>
		public static int Inverse(int layer) => ~layer;
		
		/// <summary>
		/// Returns the layers shift steps after this layer.
		/// </summary>
		public static int Next(int layer, int shift) => layer << shift;
		
		/// <summary>
		/// Returns the layers shift steps before this layer.
		/// </summary>
		public static int Previous(int layer, int shift) => layer >> shift;
	}
}
