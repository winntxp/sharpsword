﻿/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.DistributedLock.Redis
{
    [Serializable]
    public class PluginDescriptor : PluginDescriptorBase
    {
        public PluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }
    }
}
