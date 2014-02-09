#pragma once
//-------------------------------------------------------------------------------------------------
// <copyright file="userutil.h" company="Microsoft">
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
//    User helper funtions.
// </summary>
//-------------------------------------------------------------------------------------------------

#ifdef __cplusplus
extern "C" {
#endif

HRESULT DAPI UserBuildDomainUserName(
    __out_ecount_z(cchDest) LPWSTR wzDest,
    __in int cchDest,
    __in_z LPCWSTR pwzName,
    __in_z LPCWSTR pwzDomain
    );

HRESULT DAPI UserCheckIsMember(
    __in_z LPCWSTR pwzName,
    __in_z LPCWSTR pwzDomain,
    __in_z LPCWSTR pwzGroupName,
    __in_z LPCWSTR pwzGroupDomain,
    __out LPBOOL lpfMember
    );

HRESULT DAPI UserCreateADsPath(
    __in_z LPCWSTR wzObjectDomain, 
    __in_z LPCWSTR wzObjectName,
    __out BSTR *pbstrAdsPath
    );

#ifdef __cplusplus
}
#endif
