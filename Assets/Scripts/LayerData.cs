using System;

public static class LayerData
{        
	// bitwise values for layer masks
	public const int ENVIRONMENT_LAYER = 1 << 8; // layer 8
	public const int ENEMIES_LAYER = 1 << 9; // layer 9
	public const int TOWER_LAYER = 1 << 10; // layer 10
	public const int GROUND_LAYER = 1 << 11; //layer 11
	public const int PATH_LAYER = 1 << 12;

	// layers which may block tower placement
	public const int TOWER_PLACEMENT_MASK =
		ENVIRONMENT_LAYER |
		TOWER_LAYER |
		PATH_LAYER;
}