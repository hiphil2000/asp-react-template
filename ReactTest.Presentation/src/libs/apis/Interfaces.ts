export interface IMenu {
    menuId: string;
    parentId: string;
    menuName: string;
    menuPath: string;
    displayNo: number;
    menuYn: boolean;
    pageYn: boolean;
    useYn: boolean;
    visibleYn: boolean;
}

export interface IUser {
    userNo: number;
    userId: string;
    userName: string;
    role: string;
}