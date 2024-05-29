import { useSelector } from "react-redux";
import { RootState } from "./redux/store";
import { Navigate, Outlet } from "react-router-dom";

function ProtectedRouter() {
    const { userInfo } = useSelector((state: RootState) => state.userLogin);
    return userInfo?.token ? <Outlet /> : <Navigate to="/login" />
}

export { ProtectedRouter }
