using System;
using System.IO;

using IoFog.Sdk.CSharp.Extensions;
using IoFog.Sdk.CSharp.Utils;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoFog.Sdk.CSharp.Dto
{
    [JsonObject]
    public class IoMessage
    { 
        private readonly short _version = 4;

        public const string IdFieldName = "id";
        public const string TagFieldName = "tag";
        public const string GroupIdFieldName = "groupid";
        public const string SequenceNumberFieldName = "sequencenumber";
        public const string SequenceTotalFieldName = "sequencetotal";
        public const string PriorityFieldName = "priority";
        public const string TimestampFieldName = "timestamp";
        public const string PublisherFieldName = "publisher";
        public const string AuthIdFieldName = "authid";
        public const string AuthGroupFieldName = "authgroup";
        public const string VersionFieldName = "version";
        public const string ChainPositionFieldName = "chainposition";
        public const string HashFieldName = "hash";
        public const string PreviousHashFieldName = "previoushash";
        public const string NonceFieldName = "nonce";
        public const string DifficultyTargetFieldName = "difficultytarget";
        public const string InfoTypeFieldName = "infotype";
        public const string InfoFormatFieldName = "infoformat";
        public const string ContextDataFieldName = "contextdata";
        public const string ContentDataFieldName = "contentdata";

        public IoMessage()
        {
            Id = string.Empty;
            Tag = string.Empty;
            GroupId = string.Empty;
            Publisher = string.Empty;
            AuthId = string.Empty;
            AuthGroup = string.Empty;
            Version = _version;
            Hash = string.Empty;
            PreviousHash = string.Empty;
            Nonce = string.Empty;
            InfoType = string.Empty;
            InfoFormat = string.Empty;
        }

        public IoMessage(byte[] rawBytes) : this()
        {
            ConvertBytesToMessage(header: null, data: rawBytes, pos: 33);
        }

        public IoMessage(byte[] header, byte[] data) : this()
        {
            ConvertBytesToMessage(header: header, data: data, pos: 0);
        }

        public IoMessage(JObject json, bool decode) : this()
        {
            if (json[IdFieldName] != null)
                Id = json[IdFieldName].ToObject<string>();
            if (json[TagFieldName] != null)
                Tag = json[TagFieldName].ToObject<string>();
            if (json[GroupIdFieldName] != null)
                GroupId = json[GroupIdFieldName].ToObject<string>();
            if (json[SequenceNumberFieldName] != null)
                SequenceNumber = json[SequenceNumberFieldName].ToObject<int>();
            if (json[SequenceTotalFieldName] != null)
                SequenceTotal = json[SequenceTotalFieldName].ToObject<int>();
            if (json[PriorityFieldName] != null)
                Priority = json[PriorityFieldName].ToObject<sbyte>();
            if (json[TimestampFieldName] != null)
                Timestamp = json[TimestampFieldName].ToObject<long>();
            if (json[PublisherFieldName] != null)
                Publisher = json[PublisherFieldName].ToObject<string>();
            if (json[AuthIdFieldName] != null)
                AuthId = json[AuthIdFieldName].ToObject<string>();
            if (json[AuthGroupFieldName] != null)
                AuthGroup = json[AuthGroupFieldName].ToObject<string>();
            if (json[VersionFieldName] != null)
                Version = json[VersionFieldName].ToObject<short>();
            if (json[ChainPositionFieldName] != null)
                ChainPosition = json[ChainPositionFieldName].ToObject<long>();
            if (json[HashFieldName] != null)
                Hash = json[HashFieldName].ToObject<string>();
            if (json[PreviousHashFieldName] != null)
                PreviousHash = json[PreviousHashFieldName].ToObject<string>();
            if (json[NonceFieldName] != null)
                Nonce = json[NonceFieldName].ToObject<string>();
            if (json[DifficultyTargetFieldName] != null)
                DifficultyTarget = json[DifficultyTargetFieldName].ToObject<int>();
            if (json[InfoTypeFieldName] != null)
                InfoType = json[InfoTypeFieldName].ToObject<string>();
            if (json[InfoFormatFieldName] != null)
                InfoFormat = json[InfoFormatFieldName].ToObject<string>();
            if (json[ContextDataFieldName] != null)
            {
                string contextDataString = json[ContextDataFieldName].ToObject<string>();
                if (decode)
                {
                    ContextData = IoMessageUtils.DecodeBase64(contextDataString.GetBytes());
                }
                else
                {
                    ContextData = contextDataString.GetBytes();
                }
            }

            if (json[ContentDataFieldName] != null)
            {
                string contentDataString = json[ContentDataFieldName].ToObject<string>();
                if (decode)
                {
                    ContentData = IoMessageUtils.DecodeBase64(contentDataString.GetBytes());
                }
                else
                {
                    ContentData = contentDataString.GetBytes();
                }
            }
            
        }

        [JsonProperty(IdFieldName)]
        public string Id { get; set; }

        [JsonProperty(TagFieldName)]
        public string Tag { get; set; }

        [JsonProperty(GroupIdFieldName)]
        public string GroupId { get; set; }

        [JsonProperty(SequenceNumberFieldName)]
        public int SequenceNumber { get; set; }

        [JsonProperty(SequenceTotalFieldName)]
        public int SequenceTotal { get; set; }

        [JsonProperty(PriorityFieldName)]
        public sbyte Priority { get; set; }

        [JsonProperty(TimestampFieldName)]
        public long Timestamp { get; set; }

        [JsonProperty(PublisherFieldName)]
        public string Publisher { get; internal set; }

        [JsonProperty(AuthIdFieldName)]
        public string AuthId { get; set; }

        [JsonProperty(AuthGroupFieldName)]
        public string AuthGroup { get; set; }

        [JsonProperty(VersionFieldName)]
        public short Version { get; private set; }

        [JsonProperty(ChainPositionFieldName)]
        public long ChainPosition { get; set; }

        [JsonProperty(HashFieldName)]
        public string Hash { get; set; }

        [JsonProperty(PreviousHashFieldName)]
        public string PreviousHash { get; set; }

        [JsonProperty(NonceFieldName)]
        public string Nonce { get; set; }

        [JsonProperty(DifficultyTargetFieldName)]
        public int DifficultyTarget { get; set; }

        [JsonProperty(InfoTypeFieldName)]
        public string InfoType { get; set; }

        [JsonProperty(InfoFormatFieldName)]
        public string InfoFormat { get; set; }

        [JsonProperty(ContextDataFieldName)]
        public byte[] ContextData { get; set; }

        [JsonProperty(ContentDataFieldName)]
        public byte[] ContentData { get; set; }


        // Never used
        public JObject ToJson()
        {
            var json = new JObject
            {
                {IdFieldName, Id ?? string.Empty},
                {TagFieldName, Tag ?? string.Empty},
                {GroupIdFieldName, GroupId ?? string.Empty},
                {SequenceNumberFieldName, SequenceNumber},
                {SequenceTotalFieldName, SequenceTotal},
                {PriorityFieldName, SequenceNumber},
                {TimestampFieldName, SequenceTotal},
                {PublisherFieldName, Publisher ?? string.Empty},
                {AuthIdFieldName, AuthId ?? string.Empty},
                {AuthGroupFieldName, AuthGroup ?? string.Empty},
                {VersionFieldName, Version},
                {ChainPositionFieldName, ChainPosition},
                {HashFieldName, Hash ?? string.Empty},
                {PreviousHashFieldName, PreviousHash ?? string.Empty},
                {NonceFieldName, Nonce ?? string.Empty},
                {DifficultyTargetFieldName, DifficultyTarget},
                {InfoTypeFieldName, Publisher ?? string.Empty},
                {InfoFormatFieldName, AuthId ?? string.Empty},
                {ContextDataFieldName, ContextData == null ? string.Empty : StringUtils.NewString(ContextData)},
                {ContentDataFieldName, ContentData == null ? string.Empty : StringUtils.NewString(ContentData)}
            };

            return json;
        }

        public JObject GetJson(bool encoded)
        {
            string contextData = string.Empty;
            string contentData = string.Empty;

            byte[] data = ContextData;

            if (data != null)
            {
                if (encoded)
                {
                    data = IoMessageUtils.EncodeBase64(data);
                }
                contextData = StringUtils.NewString(data);
            }

            data = ContentData;
            if (data != null)
            {
                if (encoded)
                {
                    data = IoMessageUtils.EncodeBase64(data);
                }
                contentData = StringUtils.NewString(data);
            }

            var json = new JObject
            {
                {IdFieldName, Id},
                {TagFieldName, Tag},
                {GroupIdFieldName, GroupId},
                {SequenceNumberFieldName, SequenceNumber},
                {SequenceTotalFieldName, SequenceTotal},
                {PriorityFieldName, Priority},
                {TimestampFieldName, Timestamp},
                {PublisherFieldName, Publisher},
                {AuthIdFieldName, AuthId},
                {AuthGroupFieldName, AuthGroup},
                {VersionFieldName, Version},
                {ChainPositionFieldName, ChainPosition},
                {HashFieldName, Hash},
                {PreviousHashFieldName, PreviousHash},
                {NonceFieldName, Nonce},
                {DifficultyTargetFieldName, DifficultyTarget},
                {InfoTypeFieldName, InfoType},
                {InfoFormatFieldName, InfoFormat},
                {ContextDataFieldName, contextData},
                {ContentDataFieldName, contentData}
            };

            return json;
        }

        public byte[] GetBytes()
        {
            using (MemoryStream headerStream = new MemoryStream(), dataStream = new MemoryStream())
            {
                BinaryWriter headerBaos = new BinaryWriter(headerStream);
                BinaryWriter dataBaos = new BinaryWriter(dataStream);

                try
                {
                    // Version
                    headerBaos.Write(ByteUtils.ShortToBytes(_version));

                    // Id
                    int len = Id.GetLengthOrZero();
                    headerBaos.Write((byte)(len & 0xff));
                    if (len > 0)
                    {
                        dataBaos.Write(Id.GetBytesOrEmptyArray());
                    }

                    // Tag
                    len = Tag.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(Tag.GetBytesOrEmptyArray());
                    }

                    // GroupId
                    len = GroupId.GetLengthOrZero();
                    headerBaos.Write((byte)(len & 0xff));
                    if (len > 0)
                    {
                        dataBaos.Write(GroupId.GetBytesOrEmptyArray());
                    }

                    // SequenceNumber
                    if (SequenceNumber == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)4);
                        dataBaos.Write(ByteUtils.IntegerToBytes(SequenceNumber));
                    }

                    // SequenceTotal
                    if (SequenceTotal == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)4);
                        dataBaos.Write(ByteUtils.IntegerToBytes(SequenceTotal));
                    }

                    // Priority
                    if (Priority == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)1);
                        dataBaos.Write(Priority);
                    }

                    //Timestamp
                    if (Timestamp == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)8);
                        dataBaos.Write(ByteUtils.LongToBytes(Timestamp));
                    }

                    // Publisher
                    len = Publisher.GetLengthOrZero();
                    headerBaos.Write((byte)(len & 0xff));
                    if (len > 0)
                    {
                        dataBaos.Write(Publisher.GetBytesOrEmptyArray());
                    }

                    // AuthId
                    len = AuthId.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(AuthId.GetBytesOrEmptyArray());
                    }

                    // AuthGroup
                    len = AuthGroup.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(AuthGroup.GetBytesOrEmptyArray());
                    }

                    // ChainPosition
                    if (ChainPosition == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)8);
                        dataBaos.Write(ByteUtils.LongToBytes(ChainPosition));
                    }

                    // Hash
                    len = Hash.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(Hash.GetBytesOrEmptyArray());
                    }

                    // PreviousHash
                    len = PreviousHash.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(PreviousHash.GetBytesOrEmptyArray());
                    }

                    // Nonce
                    len = Nonce.GetLengthOrZero();
                    headerBaos.Write(ByteUtils.ShortToBytes((short)(len & 0xffff)));
                    if (len > 0)
                    {
                        dataBaos.Write(Nonce.GetBytesOrEmptyArray());
                    }

                    // DifficultyTarget
                    if (DifficultyTarget == 0)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write((byte)4);
                        dataBaos.Write(ByteUtils.IntegerToBytes(DifficultyTarget));
                    }

                    // InfoType
                    len = InfoType.GetLengthOrZero();
                    headerBaos.Write((byte)(len & 0xff));
                    if (len > 0)
                    {
                        dataBaos.Write(InfoType.GetBytesOrEmptyArray());
                    }

                    // InfoFormat
                    len = InfoFormat.GetLengthOrZero();
                    headerBaos.Write((byte)(len & 0xff));
                    if (len > 0)
                    {
                        dataBaos.Write(InfoFormat.GetBytesOrEmptyArray());
                    }

                    // ContextData
                    if (ContextData == null)
                    {
                        headerBaos.Write((byte)0);
                    }
                    else
                    {
                        headerBaos.Write(ByteUtils.IntegerToBytes(ContextData.Length));
                        dataBaos.Write(ContextData, 0, ContextData.Length);
                    }

                    // ContentData
                    if (ContentData == null)
                    {
                        headerBaos.Write(ByteUtils.IntegerToBytes(0));
                    }
                    else
                    {
                        headerBaos.Write(ByteUtils.IntegerToBytes(ContentData.Length));
                        dataBaos.Write(ContentData, 0, ContentData.Length);
                    }

                    var headerBytes = headerStream.ToArray();
                    var dataBytes = dataStream.ToArray();

                    var result = new byte[headerBytes.Length + dataBytes.Length];
                    Array.Copy(headerBytes, 0, result, 0, headerBytes.Length);
                    Array.Copy(dataBytes, 0, result, headerBytes.Length, dataBytes.Length);

                    return result;
                }
                catch (Exception)
                {
                    //TODO: log
                }
                finally
                {
                    try
                    {
                        headerBaos.Dispose();
                        dataBaos.Dispose();
                    }
                    catch (Exception)
                    {
                        // TODO: log("Error when closing byte arrays streams");
                    }
                }
            }

            return new byte[0];
        }

        public override string ToString()
        {
            return GetJson(false).ToString();
        }

        private void ConvertBytesToMessage(byte[] header, byte[] data, int pos)
        {
            if (header == null || header.Length == 0)
            {
                header = data;
            }

            Version = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 0, 2));

            if (Version != _version)
            {
                // TODO: incompatible version
                throw new Exception("Incompatible version");
            }

            int size = header[2];
            if (size > 0)
            {
                Id = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 3, 5));
            if (size > 0)
            {
                Tag = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[5];
            if (size > 0)
            {
                GroupId = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[6];
            if (size > 0)
            {
                SequenceNumber = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[7];
            if (size > 0)
            {
                SequenceTotal = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[8];
            if (size > 0)
            {
                Priority = (sbyte)data[pos];
                pos += size;
            }

            size = header[9];
            if (size > 0)
            {
                Timestamp = ByteUtils.BytesToLong(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[10];
            if (size > 0)
            {
                Publisher = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 11, 13));
            if (size > 0)
            {
                AuthId = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 13, 15));
            if (size > 0)
            {
                AuthGroup = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[15];
            if (size > 0)
            {
                ChainPosition = ByteUtils.BytesToLong(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 16, 18));
            if (size > 0)
            {
                Hash = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 18, 20));
            if (size > 0)
            {
                PreviousHash = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToShort(ByteUtils.CopyOfRange(header, 20, 22));
            if (size > 0)
            {
                Nonce = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[22];
            if (size > 0)
            {
                DifficultyTarget = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[23];
            if (size > 0)
            {
                InfoType = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = header[24];
            if (size > 0)
            {
                InfoFormat = StringUtils.NewString(ByteUtils.CopyOfRange(data, pos, pos + size));
                pos += size;
            }

            size = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(header, 25, 29));
            if (size > 0)
            {
                ContextData = ByteUtils.CopyOfRange(data, pos, pos + size);
                pos += size;
            }

            size = ByteUtils.BytesToInteger(ByteUtils.CopyOfRange(header, 29, 33));
            if (size > 0)
            {
                ContentData = ByteUtils.CopyOfRange(data, pos, pos + size);
            }
        }
    }
}
