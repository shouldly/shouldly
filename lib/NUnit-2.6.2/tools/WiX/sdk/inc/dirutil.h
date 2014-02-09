//-------------------------------------------------------------------------------------------------
// <copyright file="dirutil.h" company="Microsoft">
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
//    Directory helper funtions.
// </summary>
//-------------------------------------------------------------------------------------------------

#pragma once

#ifdef __cplusplus
extern "C" {
#endif

BOOL DAPI DirExists(
    __in_z LPCWSTR wzPath, 
    __out_opt DWORD *pdwAttributes
    );

HRESULT DAPI DirCreateTempPath(
    __in_z LPCWSTR wzPrefix,
    __out_ecount_z(cchPath) LPWSTR wzPath,
    __in DWORD cchPath
    );

HRESULT DAPI DirEnsureExists(
    __in_z LPCWSTR wzPath, 
    __in_opt LPSECURITY_ATTRIBUTES psa
    );

HRESULT DAPI DirEnsureDelete(
    __in_z LPCWSTR wzPath,
    __in BOOL fDeleteFiles,
    __in BOOL fRecurse
    );

HRESULT DAPI DirGetCurrent(
    __deref_out_z LPWSTR* psczCurrentDirectory
    );

HRESULT DAPI DirSetCurrent(
    __in_z LPCWSTR wzDirectory
    );

#ifdef __cplusplus
}
#endif

