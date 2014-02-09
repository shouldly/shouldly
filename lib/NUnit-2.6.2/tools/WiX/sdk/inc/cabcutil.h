#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="cabcutil.h" company="Microsoft">
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
//    Header for cabinet creation helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#include <fci.h>
#include <fcntl.h>
#include <msi.h>

#define CAB_MAX_SIZE 0x7FFFFFFF   // (see KB: Q174866)

#ifdef __cplusplus
extern "C" {
#endif

// time vs. space trade-off
enum COMPRESSION_TYPE 
{ 
    COMPRESSION_TYPE_NONE, // fastest
    COMPRESSION_TYPE_LOW, 
    COMPRESSION_TYPE_MEDIUM,
    COMPRESSION_TYPE_HIGH, // smallest
    COMPRESSION_TYPE_MSZIP
};

// functions
HRESULT DAPI CabCBegin(
    __in_z LPCWSTR wzCab,
    __in_z LPCWSTR wzCabDir,
    __in DWORD dwMaxFiles,
    __in DWORD dwMaxSize,
    __in DWORD dwMaxThresh,
    __in COMPRESSION_TYPE ct,
    __out HANDLE *phContext
    );
HRESULT DAPI CabCNextCab(
    __in HANDLE hContext
    );
HRESULT DAPI CabCAddFile(
    __in_z LPCWSTR wzFile,
    __in_z_opt LPCWSTR wzToken,
    __in_opt PMSIFILEHASHINFO pmfHash,
    __in HANDLE hContext
    );
HRESULT DAPI CabCFinish(
    __in HANDLE hContext
    );
void DAPI CabCCancel(
    __in HANDLE hContext
    );

#ifdef __cplusplus
}
#endif
