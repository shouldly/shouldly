#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="dictutil.h" company="Microsoft">
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
//    Header for string dict helper functions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

typedef void* STRINGDICT_HANDLE;

HRESULT DAPI DictCreate(
    __out void **ppvHandle,
    __in DWORD dwNumExpectedItems,
    __in size_t cByteOffset
    );
HRESULT DAPI DictAdd(
    __in void *pvHandle,
    __in_z LPCWSTR szString,
    __in void *pvValue
    );
HRESULT DAPI DictGet(
    __in void *pvHandle,
    __in_z LPCWSTR szString,
    __out void **ppvValue
    );
void DAPI DictDestroy(
    __in void *pvHandle
    );

#ifdef __cplusplus
}
#endif
