namespace FSA_3S.Enum
{
    public enum ContractStatusEnum
    {
        Active = 1, // đang trong thời gian thực hiện hợp đồng
        Completed = 2, // Done hợp đồng vd: đã mua thành công Bđs
        Canceled = 3, // Đang thuê nhà 1 năm nhưng mà ở có nửa năm xong đi chỗ khác =))
        Expired = 4, // Hợp đồng cho thuê 1 năm đã hết hạn ...
    }
}