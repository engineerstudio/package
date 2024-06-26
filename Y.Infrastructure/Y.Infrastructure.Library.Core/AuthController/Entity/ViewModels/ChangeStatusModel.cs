﻿using System;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class ChangeStatusModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 修改后的状态
        /// </summary>
        public Boolean Status { get; set; }
    }
}