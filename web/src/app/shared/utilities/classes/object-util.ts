
export class ObjectUtil {
  public static copyTo<T>(source: T, target: T): void {
    for (const key in source) {
      if (source.hasOwnProperty(key)) {
        target[key] = source[key];
      }
    }
  }

  public static createCopy<T>(source: T): T {
    const objectCopy = <T>{};
    ObjectUtil.copyTo(source, objectCopy);
    return objectCopy;
  }

  public static duplicateArray(source: any[]) {
    const arr = [];
    source.forEach((x) => {
      arr.push((<any>Object).assign({}, x));
    });
    return arr;
  }

  /**
   * @deprecated (Use common-function clone)
   */
  public static clone(object: any): any {
    return JSON.parse(JSON.stringify(object));
  }

  /**
   * @deprecated (Use common-function copyArray)
   */
  public static copyArray<T>(source: T[]): T[] {
    return ObjectUtil.clone(source);
  }

  /**
   * @deprecated (Use common-function copy)
   */
  public static copy<T>(source: T): T {
    return Object.assign({}, source);
  }
}
