#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="metautil.h" company="Microsoft">
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
//    IIS Metabase helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#include <iadmw.h>
#include <iiscnfg.h>
#include <iwamreg.h>
#include <mddefw.h>

#ifdef __cplusplus
extern "C" {
#endif

// structs

// prototypes
HRESULT DAPI MetaFindWebBase(
    __in IMSAdminBaseW* piMetabase, 
    __in_z LPCWSTR wzIP, 
    __in int iPort, 
    __in_z LPCWSTR wzHeader,
    __in BOOL fSecure,
    __out_ecount(cchWebBase) LPWSTR wzWebBase, 
    __in DWORD cchWebBase
    );
HRESULT DAPI MetaFindFreeWebBase(
    __in IMSAdminBaseW* piMetabase, 
    __out_ecount(cchWebBase) LPWSTR wzWebBase, 
    __in DWORD cchWebBase
    );

HRESULT DAPI MetaOpenKey(
    __in IMSAdminBaseW* piMetabase, 
    __in METADATA_HANDLE mhKey, 
    __in_z LPCWSTR wzKey,
    __in DWORD dwAccess,
    __in DWORD cRetries,
    __out METADATA_HANDLE* pmh
    );
HRESULT DAPI MetaGetValue(
    __in IMSAdminBaseW* piMetabase, 
    __in METADATA_HANDLE mhKey, 
    __in_z LPCWSTR wzKey, 
    __inout METADATA_RECORD* pmr
    );
void DAPI MetaFreeValue(
    __in METADATA_RECORD* pmr
    );

#ifdef __cplusplus
}
#endif
