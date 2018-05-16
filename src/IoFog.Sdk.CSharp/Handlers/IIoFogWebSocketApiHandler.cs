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

using IoFog.Sdk.CSharp.Dto;

namespace IoFog.Sdk.CSharp.Handlers
{

    /// <summary>
    /// Listener's Interface for requests to ioFog.
    /// </summary>
    public interface IIoFogWebSocketApiHandler
    {

        /// <summary>
        /// Method is triggered when Container receives messages.
        /// </summary>
        /// <param name="message">Received message</param>
        void OnMessage(IoMessage message);

        /// <summary>
        /// Method is triggered when Container catches an exception.
        /// </summary>
        /// <param name="exception">Exception</param>
        void OnException(Exception exception);

        /// <summary>
        /// Method is triggered when Container receives a signal about new configuration.
        /// </summary>
        void OnNewConfigSignal();

        /// <summary>
        /// Method is triggered when Container receives a signal just sended message.
        /// </summary>
        /// <param name="messageId">Message id</param>
        /// <param name="timestamp">Timestamp</param>
        void OnReceipt(string messageId, long timestamp);
    }
}
