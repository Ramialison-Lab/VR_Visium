// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
    using System;

    public class LaunchReportFlowResult
    {
        public readonly bool DidCancel;
        public readonly UInt64 UserReportId;


        public LaunchReportFlowResult(IntPtr o)
        {
            DidCancel = CAPI.ovr_LaunchReportFlowResult_GetDidCancel(o);
            UserReportId = CAPI.ovr_LaunchReportFlowResult_GetUserReportId(o);
        }
    }

}
