export class TypeGuardUtil {

  public static isBoolean(x: any): x is boolean {
    return typeof x === 'boolean';
  }

  public static isNumber(x: any): x is number {
    return typeof x === 'number';
  }

  public static isString(x: any): x is string {
    return typeof x === 'string';
  }

  public static isDate(x: any): x is Date {
    return Object.prototype.toString.call(x) === '[object Date]';
  }

  public static isNullOrUndefined(x: any): boolean {
    return x === null || x === undefined || (TypeGuardUtil.isString(x) && x.length === 0);
  }

  public static isArrayNullOrEmpty(x: any[]): boolean {
    if (this.isArray(x)) {
      return x === null || x === undefined || x.length === 0;
    } else {
      return x === null || x === undefined;
    }
  }

  public static isFunction(x: any): boolean {
    return typeof x === 'function';
  }

  public static isArray(x: any): boolean {
    return x instanceof Array;
  }

  public static toUpper(x: string): string {
    if (TypeGuardUtil.isString(x)) {
      return x.toUpperCase();
    }
  }
}
