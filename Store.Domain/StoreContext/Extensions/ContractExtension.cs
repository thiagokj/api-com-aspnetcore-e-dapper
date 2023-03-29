using System.Text.RegularExpressions;
using Flunt.Validations;
using Store.Domain.StoreContext.Utils;

namespace Store.Domain.StoreContext.Extensions;
public static class ContractExtension
{
    public static Contract<T> IsPhone<T>(this Contract<T> contract, string val, string key, string message)
    {
        var phoneNumber = new string(val.Where(char.IsDigit).ToArray());

        if (!Regex.IsMatch(phoneNumber ?? "", @"^\d{11}$"))
        {
            contract.AddNotification(key, message);
            return contract;
        }

        return contract;
    }

    public static Contract<T> IsCPF<T>(this Contract<T> contract, string val, string key, string message)
    {

        if (!DocumentsBR.IsValidCPF(val))
        {
            contract.AddNotification(key, message);
            return contract;
        }

        return contract;
    }

    public static Contract<T> IsName<T>(
        this Contract<T> contract,
        string val,
        int start,
        int end,
        string key,
        string message)
    {
        if (val == null || val.Length == 0)
        {
            contract.AddNotification(key, message);
            return contract;
        }

        if (val.Length < start || val.Length > end)
        {
            contract.AddNotification(key, message);
            return contract;
        }

        return contract;
    }
}

