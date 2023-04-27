import { render, screen, cleanup } from "@testing-library/react";
import { mount, ReactWrapper } from "enzyme";
import React from "react";
import Posts from "../pages/Posts";
import { configure } from "enzyme";
import Adapter from "enzyme-adapter-react-16";

configure({ adapter: new Adapter() });

describe("Posts Component", () => {
    let wrapper: ReactWrapper;
    let component: JSX.Element;

    jest.spyOn(React, "useEffect").mockImplementation(() => { return; });

    beforeEach(() => {
        component = <Posts />;
        wrapper = mount(component);
    });

    afterEach(() => {
        wrapper.unmount();
    });

    it("should load component successfully", () => {
        expect(wrapper.contains(component)).toBeTruthy();
    });
});