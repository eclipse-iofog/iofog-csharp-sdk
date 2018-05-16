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

namespace IoFog.Sdk.CSharp.Dto
{
    internal enum IoFogWebSocketOpCodeEnum : byte
    {
        Ping = 0x9,
        Pong = 0xA,
        Ack = 0xB,
        ControlSignal = 0xC,
        Message = 0xD,
        Receipt = 0xE
    }
}
