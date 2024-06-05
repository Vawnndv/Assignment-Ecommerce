import { useSelector } from "react-redux";
import { AppDispatch, RootState, useAppDispatch } from "./redux/store";
import { Navigate, Outlet } from "react-router-dom";
import { logoutAction } from "./redux/actions/authActions";
import { toast } from "react-toastify";

function ProtectedRouter() {

    const MyDispatch: AppDispatch = useAppDispatch();
    const { userInfo } = useSelector((state: RootState) => state.userLogin);
    return userInfo?.token ? (
        userInfo?.role === "Admin" ? <Outlet /> : (
            MyDispatch(logoutAction()),
            toast.error("You do not have permission to access this Website")
        )
    ) : (
        <Navigate to="/Login" />
    )
}

export { ProtectedRouter }
