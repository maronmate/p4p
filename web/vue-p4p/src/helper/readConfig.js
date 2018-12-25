import configFile from '@/config.json';

function getConfigValue(key)
{
        var hasKey = configFile.hasOwnProperty(key);
        if(hasKey)
        {
            var value = configFile[key];
            return value;
        }
}
export function getApiHost()
{
    return getConfigValue('apiHostUrl');
}
export function getKeyLocal()
{
    return getConfigValue('localStorageKey');
}
export function getPageSize()
{
    return getConfigValue('pagingSize');
}