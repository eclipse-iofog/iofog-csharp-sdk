/*
 *******************************************************************************
 * Copyright (c) 2018 Edgeworx, Inc.
 *
 * This program and the accompanying materials are made available under the
 * terms of the Eclipse Public License v. 2.0 which is available at
 * http://www.eclipse.org/legal/epl-2.0
 *
 * SPDX-License-Identifier: EPL-2.0
 *******************************************************************************
*/

using System;

namespace IoFog.Sdk.CSharp.Handlers
{
    public interface IIoFogRestApiHandler
    {
        /// <summary>
        /// Method is triggered when Container catches an exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        void OnException(Exception exception);

        /// <summary>
        /// Method is triggered when Container receives Bad Request (400) response from ioFog.
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        void OnBadRequest(string errorMessage);
    }
}
