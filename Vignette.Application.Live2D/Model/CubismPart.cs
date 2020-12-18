﻿// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System;
using Vignette.Application.Live2D.Id;

namespace Vignette.Application.Live2D.Model
{
    public class CubismPart : CubismId
    {
        public float Target { get; set; }

        private float val;

        public float Value
        {
            get => val;
            set => val = Math.Clamp(value, 0, 1);
        }

        public CubismPart(int index, string name)
            : base(index, name)
        {
        }
    }
}
