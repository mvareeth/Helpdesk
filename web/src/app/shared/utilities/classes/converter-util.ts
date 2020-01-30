import { TypeGuardUtil } from './../../utilities/classes/type-guard-util';
export class ConverterUtil {
    public static arrayToQuerysting(arrayName: string, arrayObject: any[]): string {
        //https://stackoverflow.com/questions/3061273/send-an-array-with-an-http-get
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding
        let querysting = '';
        for (let i = 0; i < arrayObject.length; i++) {
            if (querysting.indexOf('?') === -1) {
                querysting += '?' + arrayName + '=' + arrayObject[i];
            } else {
                querysting += '&' + arrayName + '=' + arrayObject[i];
            }
        }
        return querysting;
    }

    // must allow ToString on objects
    // anticipated for number[] source
    public static arrayToDelimitedString(arrayObject: any[], delimiter: string = ','): string {
        let resultString: string = '';
        let isFirstItem: boolean = true;
        if (!TypeGuardUtil.isNullOrUndefined(arrayObject)) {
            arrayObject.forEach(item => {
                if (!isFirstItem) {
                    resultString += delimiter;
                }
                resultString += String(item).trim();
                isFirstItem = false;
            });
        }
        return resultString;
    }

    public static delimitedStringToIntArray(stringList: string, delimiter: string = ','): number[] {
        let resultArray: number[] = [];
        try {
            resultArray = stringList.split(delimiter).map(Number);
        } catch (ex) {
            alert('ConverterUtil.delimitedStringToArray: Could not parse list provided.');
        }
        return resultArray;
    }
}