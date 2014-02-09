#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="perfutil.h" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
//    
//    The use and distribution terms for this software are covered by the
//    Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)
//    which can be found in the file CPL.TXT at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//    
//    You must not remove this notice, or any other, from this software.
// </copyright>
// 
// <summary>
//    Performance helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

// structs


// functions
void DAPI PerfInitialize(
    );
void DAPI PerfClickTime(
    __out_opt LARGE_INTEGER* pliElapsed
    );
double DAPI PerfConvertToSeconds(
    __in const LARGE_INTEGER* pli
    );

#ifdef __cplusplus
}
#endif
