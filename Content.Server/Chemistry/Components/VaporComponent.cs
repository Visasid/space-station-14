﻿using Content.Shared.FixedPoint;

namespace Content.Server.Chemistry.Components
{
    [RegisterComponent]
    public sealed class VaporComponent : Component
    {
        public const string SolutionName = "vapor";

        [DataField("transferAmount")]
        public FixedPoint2 TransferAmount = FixedPoint2.New(0.5);

        public float ReactTimer;
        public bool Active;
    }
}
